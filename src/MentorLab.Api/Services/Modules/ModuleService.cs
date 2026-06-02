using MentorLab.Api.Data;
using MentorLab.Api.DTOs.Modules;
using MentorLab.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentorLab.Api.Services.Modules;

public class ModuleService : IModuleService
{
    private readonly MentorLabDbContext _dbContext;

    public ModuleService(MentorLabDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ModuleResponse>?> GetByLearningTrackAsync(int learningTrackId)
    {
        var trackExists = await _dbContext.LearningTracks
            .AsNoTracking()
            .AnyAsync(track => track.Id == learningTrackId && track.IsActive);

        if (!trackExists)
        {
            return null;
        }

        return await _dbContext.Modules
            .AsNoTracking()
            .Where(item => item.LearningTrackId == learningTrackId && item.IsActive)
            .OrderBy(item => item.Order)
            .ThenBy(item => item.Title)
            .Select(item => ToResponse(item))
            .ToListAsync();
    }

    public async Task<ModuleResponse?> GetByIdAsync(int id)
    {
        var item = await _dbContext.Modules
            .AsNoTracking()
            .FirstOrDefaultAsync(module => module.Id == id && module.IsActive);

        return item is null ? null : ToResponse(item);
    }

    public async Task<ModuleResponse?> CreateAsync(int learningTrackId, CreateModuleRequest request)
    {
        ValidateData(request.Title, request.DisplayOrder);

        var trackExists = await _dbContext.LearningTracks
            .AnyAsync(track => track.Id == learningTrackId && track.IsActive);

        if (!trackExists)
        {
            return null;
        }

        var item = new Module
        {
            LearningTrackId = learningTrackId,
            Title = request.Title.Trim(),
            Description = NormalizeDescription(request.Description),
            Order = request.DisplayOrder,
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _dbContext.Modules.Add(item);
        await _dbContext.SaveChangesAsync();

        return ToResponse(item);
    }

    public async Task<ModuleResponse?> UpdateAsync(int id, UpdateModuleRequest request)
    {
        ValidateData(request.Title, request.DisplayOrder);

        var item = await _dbContext.Modules
            .FirstOrDefaultAsync(module => module.Id == id && module.IsActive);

        if (item is null)
        {
            return null;
        }

        item.Title = request.Title.Trim();
        item.Description = NormalizeDescription(request.Description);
        item.Order = request.DisplayOrder;
        item.UpdatedAt = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();

        return ToResponse(item);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _dbContext.Modules
            .FirstOrDefaultAsync(module => module.Id == id && module.IsActive);

        if (item is null)
        {
            return false;
        }

        item.IsActive = false;
        item.UpdatedAt = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    private static ModuleResponse ToResponse(Module item)
    {
        return new ModuleResponse(
            item.Id,
            item.LearningTrackId,
            item.Title,
            item.Description,
            item.Order,
            item.IsActive,
            item.CreatedAt,
            item.UpdatedAt
        );
    }

    private static void ValidateData(string title, int displayOrder)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Título do módulo é obrigatório.");
        }

        if (displayOrder <= 0)
        {
            throw new ArgumentException("A ordem de exibição deve ser maior que zero.");
        }
    }

    private static string? NormalizeDescription(string? description)
    {
        return string.IsNullOrWhiteSpace(description) ? null : description.Trim();
    }
}
