namespace MentorLab.Api.Dtos;

public record UpdateStudentRequest(
    string FullName,
    string Email,
    string? Phone,
    string? GitHubUsername,
    bool IsActive
);
