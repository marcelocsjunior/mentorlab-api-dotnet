namespace MentorLab.Api.DTOs.Modules;

public record CreateModuleRequest(
    string Title,
    string? Description,
    int DisplayOrder
);
