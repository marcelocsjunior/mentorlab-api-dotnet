namespace MentorLab.Api.Dtos;

public record StudentResponse(
    Guid Id,
    string FullName,
    string Email,
    string? Phone,
    string? GitHubUsername,
    bool IsActive,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? UpdatedAtUtc
);
