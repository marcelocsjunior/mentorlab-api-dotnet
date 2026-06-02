namespace MentorLab.Api.DTOs.Modules;

public record ModuleResponse(
    int Id,
    int LearningTrackId,
    string Title,
    string? Description,
    int DisplayOrder,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
);
