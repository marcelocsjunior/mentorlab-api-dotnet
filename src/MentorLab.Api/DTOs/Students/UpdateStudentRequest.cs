namespace MentorLab.Api.DTOs.Students;

public record UpdateStudentRequest(
    string FullName,
    string Email
);
