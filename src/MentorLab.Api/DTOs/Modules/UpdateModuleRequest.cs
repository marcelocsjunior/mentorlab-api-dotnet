namespace MentorLab.Api.DTOs.Modules;

public record UpdateModuleRequest(
    string Title,
    string? Description,
    int DisplayOrder
);
