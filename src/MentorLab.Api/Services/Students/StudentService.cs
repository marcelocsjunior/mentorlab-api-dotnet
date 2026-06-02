using MentorLab.Api.Data;
using MentorLab.Api.DTOs.Students;
using MentorLab.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentorLab.Api.Services.Students;

public class StudentService : IStudentService
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

        var email = NormalizeEmail(request.Email);
        if (await ActiveEmailExistsAsync(email))
        {
            throw new InvalidOperationException("Já existe aluno ativo cadastrado com este e-mail.");
        }

        var student = new Student
        {
            FullName = request.FullName.Trim(),
            Email = email,
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

        var email = NormalizeEmail(request.Email);
        if (await ActiveEmailExistsAsync(email, id))
        {
            throw new InvalidOperationException("Já existe aluno ativo cadastrado com este e-mail.");
        }

        student.FullName = request.FullName.Trim();
        student.Email = email;
        student.UpdatedAt = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();

        return ToResponse(student);
    }

    public async Task<bool> DeleteAsync(int id)
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

    private async Task<bool> ActiveEmailExistsAsync(string email, int? ignoredStudentId = null)
    {
        return await _dbContext.Students.AnyAsync(student =>
            student.IsActive &&
            student.Email == email &&
            (!ignoredStudentId.HasValue || student.Id != ignoredStudentId.Value));
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

    private static void ValidateStudentData(string fullName, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Nome completo é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("E-mail é obrigatório.");
        }

        var trimmedEmail = email.Trim();
        if (!trimmedEmail.Contains('@') || trimmedEmail.StartsWith('@') || trimmedEmail.EndsWith('@'))
        {
            throw new ArgumentException("Formato de e-mail inválido.");
        }
    }

    private static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }
}
