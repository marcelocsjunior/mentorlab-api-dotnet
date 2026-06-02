using System.Net.Mail;
using MentorLab.Api.Data;
using MentorLab.Api.Dtos;
using MentorLab.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentorLab.Api.Services;

public class StudentService
{
    private readonly MentorLabDbContext _dbContext;

    public StudentService(MentorLabDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<StudentResponse>> GetAllAsync()
    {
        return await _dbContext.Students
            .AsNoTracking()
            .Where(student => student.IsActive)
            .OrderBy(student => student.FullName)
            .Select(student => ToResponse(student))
            .ToListAsync();
    }

    public async Task<StudentResponse?> GetByIdAsync(int id)
    {
        var student = await _dbContext.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id && item.IsActive);

        return student is null ? null : ToResponse(student);
    }

    public async Task<StudentResponse> CreateAsync(CreateStudentRequest request)
    {
        ValidateStudentData(request.FullName, request.Email);

        var normalizedEmail = NormalizeEmail(request.Email);

        if (await ActiveEmailExistsAsync(normalizedEmail))
        {
            throw new InvalidOperationException("Já existe aluno ativo cadastrado com este e-mail.");
        }

        var student = new Student
        {
            FullName = request.FullName.Trim(),
            Email = normalizedEmail,
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.Students.Add(student);
        await _dbContext.SaveChangesAsync();

        return ToResponse(student);
    }

    public async Task<StudentResponse?> UpdateAsync(int id, UpdateStudentRequest request)
    {
        ValidateStudentData(request.FullName, request.Email);

        var student = await _dbContext.Students
            .FirstOrDefaultAsync(item => item.Id == id && item.IsActive);

        if (student is null)
        {
            return null;
        }

        var normalizedEmail = NormalizeEmail(request.Email);

        if (await ActiveEmailExistsAsync(normalizedEmail, id))
        {
            throw new InvalidOperationException("Já existe outro aluno ativo cadastrado com este e-mail.");
        }

        student.FullName = request.FullName.Trim();
        student.Email = normalizedEmail;
        student.UpdatedAt = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();

        return ToResponse(student);
    }

    public async Task<bool> DeactivateAsync(int id)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(item => item.Id == id && item.IsActive);

        if (student is null)
        {
            return false;
        }

        student.IsActive = false;
        student.UpdatedAt = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    private async Task<bool> ActiveEmailExistsAsync(string normalizedEmail, int? ignoredStudentId = null)
    {
        return await _dbContext.Students.AnyAsync(student =>
            student.IsActive &&
            student.Email == normalizedEmail &&
            (!ignoredStudentId.HasValue || student.Id != ignoredStudentId.Value));
    }

    private static void ValidateStudentData(string fullName, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Nome completo é obrigatório.", nameof(fullName));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("E-mail é obrigatório.", nameof(email));
        }

        if (!IsValidEmail(email))
        {
            throw new ArgumentException("E-mail inválido.", nameof(email));
        }
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            _ = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static StudentResponse ToResponse(Student student)
    {
        return new StudentResponse(
            student.Id,
            student.FullName,
            student.Email,
            student.IsActive,
            student.CreatedAt,
            student.UpdatedAt
        );
    }

    private static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }
}
