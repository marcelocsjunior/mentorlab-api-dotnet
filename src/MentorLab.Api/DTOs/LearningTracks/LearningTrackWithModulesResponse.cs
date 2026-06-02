using MentorLab.Api.DTOs.Modules;

namespace MentorLab.Api.DTOs.LearningTracks;

public record LearningTrackWithModulesResponse(
    int Id,
    string Title,
    string? Description,
    bool IsActive,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    List<ModuleResponse> Modules
);
