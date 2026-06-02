namespace MentorLab.Api.Dtos;

public record CreateStudentRequest(
    string FullName,
    string Email,
    string? Phone,
    string? GitHubUsername
);
