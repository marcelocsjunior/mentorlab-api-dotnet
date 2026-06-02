namespace MentorLab.Api.DTOs.Students;

public record StudentResponse(
    int Id,
    string FullName,
    string Email,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
);
