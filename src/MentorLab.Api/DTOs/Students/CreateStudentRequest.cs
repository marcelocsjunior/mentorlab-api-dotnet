namespace MentorLab.Api.DTOs.Students;

public record CreateStudentRequest(
    string FullName,
    string Email
);
