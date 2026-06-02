using MentorLab.Api.DTOs.Modules;

namespace MentorLab.Api.Services.Modules;

public interface IModuleService
{
    Task<List<ModuleResponse>?> GetByLearningTrackAsync(int learningTrackId);

    Task<ModuleResponse?> GetByIdAsync(int id);

    Task<ModuleResponse?> CreateAsync(int learningTrackId, CreateModuleRequest request);

    Task<ModuleResponse?> UpdateAsync(int id, UpdateModuleRequest request);

    Task<bool> DeleteAsync(int id);
}
