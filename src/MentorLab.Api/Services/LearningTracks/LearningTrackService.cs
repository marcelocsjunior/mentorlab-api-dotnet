using MentorLab.Api.Data;
using MentorLab.Api.DTOs.LearningTracks;
using MentorLab.Api.DTOs.Modules;
using MentorLab.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentorLab.Api.Services.LearningTracks;

public class LearningTrackService : ILearningTrackService
{
    private readonly MentorLabDbContext _dbContext;

    public LearningTrackService(MentorLabDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<LearningTrackResponse>> GetAllAsync()
    {
        return await _dbContext.LearningTracks
            .AsNoTracking()
            .Where(track => track.IsActive)
            .OrderBy(track => track.Title)
            .Select(track => ToResponse(track))
            .ToListAsync();
    }

    public async Task<LearningTrackWithModulesResponse?> GetByIdAsync(int id)
    {
        var track = await _dbContext.LearningTracks
            .AsNoTracking()
            .Include(item => item.Modules.Where(module => module.IsActive))
            .FirstOrDefaultAsync(item => item.Id == id && item.IsActive);

        return track is null ? null : ToResponseWithModules(track);
    }

    public async Task<LearningTrackResponse> CreateAsync(CreateLearningTrackRequest request)
    {
        ValidateTrackData(request.Title);

        var title = request.Title.Trim();
        if (await ActiveTitleExistsAsync(title))
        {
            throw new InvalidOperationException("Já existe trilha ativa cadastrada com este título.");
        }

        var track = new LearningTrack
        {
            Title = title,
            Description = NormalizeDescription(request.Description),
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.LearningTracks.Add(track);
        await _dbContext.SaveChangesAsync();

        return ToResponse(track);
    }

    public async Task<LearningTrackResponse?> UpdateAsync(int id, UpdateLearningTrackRequest request)
    {
        ValidateTrackData(request.Title);

        var track = await _dbContext.LearningTracks
            .FirstOrDefaultAsync(item => item.Id == id && item.IsActive);

        if (track is null)
        {
            return null;
        }

        var title = request.Title.Trim();
        if (await ActiveTitleExistsAsync(title, id))
        {
            throw new InvalidOperationException("Já existe trilha ativa cadastrada com este título.");
        }

        track.Title = title;
        track.Description = NormalizeDescription(request.Description);
        track.UpdatedAt = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();

        return ToResponse(track);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var track = await _dbContext.LearningTracks
            .Include(item => item.Modules.Where(module => module.IsActive))
            .FirstOrDefaultAsync(item => item.Id == id && item.IsActive);

        if (track is null)
        {
            return false;
        }

        track.IsActive = false;
        track.UpdatedAt = DateTimeOffset.UtcNow;

        foreach (var module in track.Modules)
        {
            module.IsActive = false;
            module.UpdatedAt = DateTimeOffset.UtcNow;
        }

        await _dbContext.SaveChangesAsync();

        return true;
    }

    private async Task<bool> ActiveTitleExistsAsync(string title, int? ignoredTrackId = null)
    {
        return await _dbContext.LearningTracks.AnyAsync(track =>
            track.IsActive &&
            track.Title == title &&
            (!ignoredTrackId.HasValue || track.Id != ignoredTrackId.Value));
    }

    private static LearningTrackResponse ToResponse(LearningTrack track)
    {
        return new LearningTrackResponse(
            track.Id,
            track.Title,
            track.Description,
            track.IsActive,
            track.CreatedAt,
            track.UpdatedAt
        );
    }

    private static LearningTrackWithModulesResponse ToResponseWithModules(LearningTrack track)
    {
        var modules = track.Modules
            .Where(module => module.IsActive)
            .OrderBy(module => module.Order)
            .ThenBy(module => module.Title)
            .Select(module => new ModuleResponse(
                module.Id,
                module.LearningTrackId,
                module.Title,
                module.Description,
                module.Order,
                module.IsActive,
                module.CreatedAt,
                module.UpdatedAt
            ))
            .ToList();

        return new LearningTrackWithModulesResponse(
            track.Id,
            track.Title,
            track.Description,
            track.IsActive,
            track.CreatedAt,
            track.UpdatedAt,
            modules
        );
    }

    private static void ValidateTrackData(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Título da trilha é obrigatório.");
        }
    }

    private static string? NormalizeDescription(string? description)
    {
        return string.IsNullOrWhiteSpace(description) ? null : description.Trim();
    }
}
