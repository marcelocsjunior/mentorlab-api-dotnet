namespace MentorLab.Api.DTOs.LearningTracks;

public record LearningTrackResponse(
    int Id,
    string Title,
    string? Description,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt
);
