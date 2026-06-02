namespace MentorLab.Api.Dtos;

public record StudentResponse(Guid Id, string FullName, string Email, string? Phone, bool IsActive, DateTimeOffset CreatedAtUtc);
