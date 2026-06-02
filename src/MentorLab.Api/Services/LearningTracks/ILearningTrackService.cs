using MentorLab.Api.DTOs.LearningTracks;

namespace MentorLab.Api.Services.LearningTracks;

public interface ILearningTrackService
{
    Task<List<LearningTrackResponse>> GetAllAsync();

    Task<LearningTrackWithModulesResponse?> GetByIdAsync(int id);

    Task<LearningTrackResponse> CreateAsync(CreateLearningTrackRequest request);

    Task<LearningTrackResponse?> UpdateAsync(int id, UpdateLearningTrackRequest request);

    Task<bool> DeleteAsync(int id);
}
