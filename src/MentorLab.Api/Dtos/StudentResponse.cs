namespace MentorLab.Api.Dtos;

public record StudentResponse(
    int Id,
    string FullName,
    string Email,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
);
