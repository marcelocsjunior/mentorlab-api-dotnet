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

    public async Task<List<StudentResponse>> GetAllAsync(bool includeInactive = false)
    {
        var query = _dbContext.Students.AsNoTracking();

        if (!includeInactive)
        {
            query = query.Where(student => student.IsActive);
        }

        return await query
            .OrderBy(student => student.FullName)
            .Select(student => ToResponse(student))
            .ToListAsync();
    }

    public async Task<StudentResponse?> GetByIdAsync(Guid id)
    {
        var student = await _dbContext.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);

        return student is null ? null : ToResponse(student);
    }

    public async Task<bool> EmailExistsAsync(string email, Guid? ignoredStudentId = null)
    {
        var normalizedEmail = NormalizeEmail(email);

        return await _dbContext.Students.AnyAsync(student =>
            student.Email == normalizedEmail &&
            (!ignoredStudentId.HasValue || student.Id != ignoredStudentId.Value)
        );
    }

    public async Task<StudentResponse> CreateAsync(CreateStudentRequest request)
    {
        var student = new Student
        {
            FullName = request.FullName.Trim(),
            Email = NormalizeEmail(request.Email),
            Phone = NormalizeNullable(request.Phone),
            GitHubUsername = NormalizeNullable(request.GitHubUsername),
            IsActive = true,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };

        _dbContext.Students.Add(student);
        await _dbContext.SaveChangesAsync();

        return ToResponse(student);
    }

    public async Task<StudentResponse?> UpdateAsync(Guid id, UpdateStudentRequest request)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(item => item.Id == id);

        if (student is null)
        {
            return null;
        }

        student.FullName = request.FullName.Trim();
        student.Email = NormalizeEmail(request.Email);
        student.Phone = NormalizeNullable(request.Phone);
        student.GitHubUsername = NormalizeNullable(request.GitHubUsername);
        student.IsActive = request.IsActive;
        student.UpdatedAtUtc = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();

        return ToResponse(student);
    }

    public async Task<bool> DeactivateAsync(Guid id)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(item => item.Id == id);

        if (student is null)
        {
            return false;
        }

        student.IsActive = false;
        student.UpdatedAtUtc = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    private static StudentResponse ToResponse(Student student)
    {
        return new StudentResponse(
            student.Id,
            student.FullName,
            student.Email,
            student.Phone,
            student.GitHubUsername,
            student.IsActive,
            student.CreatedAtUtc,
            student.UpdatedAtUtc
        );
    }

    private static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }

    private static string? NormalizeNullable(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
