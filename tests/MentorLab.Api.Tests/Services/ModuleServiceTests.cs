using MentorLab.Api.DTOs.LearningTracks;
using MentorLab.Api.DTOs.Modules;
using MentorLab.Api.Tests.Infrastructure;
using MentorLab.Api.Services.LearningTracks;
using MentorLab.Api.Services.Modules;

namespace MentorLab.Api.Tests.Services;

public class ModuleServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateModuleInActiveTrack()
    {
        using var database = new SqliteTestDatabase();
        var trackService = new LearningTrackService(database.Context);
        var moduleService = new ModuleService(database.Context);
        var track = await trackService.CreateAsync(new CreateLearningTrackRequest(
            "Testes Automatizados",
            null));

        var module = await moduleService.CreateAsync(track.Id, new CreateModuleRequest(
            "xUnit",
            "Fundamentos de testes.",
            1));

        Assert.NotNull(module);
        Assert.True(module.Id > 0);
        Assert.Equal(track.Id, module.LearningTrackId);
        Assert.Equal(1, module.DisplayOrder);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnNullForMissingTrack()
    {
        using var database = new SqliteTestDatabase();
        var service = new ModuleService(database.Context);

        var module = await service.CreateAsync(999, new CreateModuleRequest(
            "Módulo órfão",
            null,
            1));

        Assert.Null(module);
    }

    [Fact]
    public async Task CreateAsync_ShouldValidateDisplayOrderGreaterThanZero()
    {
        using var database = new SqliteTestDatabase();
        var service = new ModuleService(database.Context);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateAsync(1, new CreateModuleRequest(
                "Ordem inválida",
                null,
                0)));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateModule()
    {
        using var database = new SqliteTestDatabase();
        var trackService = new LearningTrackService(database.Context);
        var moduleService = new ModuleService(database.Context);
        var track = await trackService.CreateAsync(new CreateLearningTrackRequest(
            "Atualização",
            null));
        var module = await moduleService.CreateAsync(track.Id, new CreateModuleRequest(
            "Nome inicial",
            "Descrição inicial",
            1));

        Assert.NotNull(module);
        var updated = await moduleService.UpdateAsync(module.Id, new UpdateModuleRequest(
            "Nome atualizado",
            "Descrição atualizada",
            2));

        Assert.NotNull(updated);
        Assert.Equal("Nome atualizado", updated.Title);
        Assert.Equal("Descrição atualizada", updated.Description);
        Assert.Equal(2, updated.DisplayOrder);
        Assert.NotNull(updated.UpdatedAt);
    }

    [Fact]
    public async Task DeleteAsync_ShouldApplySoftDelete()
    {
        using var database = new SqliteTestDatabase();
        var trackService = new LearningTrackService(database.Context);
        var moduleService = new ModuleService(database.Context);
        var track = await trackService.CreateAsync(new CreateLearningTrackRequest(
            "Exclusão",
            null));
        var module = await moduleService.CreateAsync(track.Id, new CreateModuleRequest(
            "Módulo removido",
            null,
            1));

        Assert.NotNull(module);
        var deleted = await moduleService.DeleteAsync(module.Id);
        var hidden = await moduleService.GetByIdAsync(module.Id);

        Assert.True(deleted);
        Assert.Null(hidden);
    }

    [Fact]
    public async Task GetByLearningTrackAsync_ShouldListActiveModulesOrderedByDisplayOrder()
    {
        using var database = new SqliteTestDatabase();
        var trackService = new LearningTrackService(database.Context);
        var moduleService = new ModuleService(database.Context);
        var track = await trackService.CreateAsync(new CreateLearningTrackRequest(
            "Ordenação",
            null));
        var third = await moduleService.CreateAsync(track.Id, new CreateModuleRequest("Terceiro", null, 3));
        var first = await moduleService.CreateAsync(track.Id, new CreateModuleRequest("Primeiro", null, 1));
        var second = await moduleService.CreateAsync(track.Id, new CreateModuleRequest("Segundo", null, 2));

        Assert.NotNull(third);
        Assert.NotNull(first);
        Assert.NotNull(second);
        await moduleService.DeleteAsync(second.Id);

        var modules = await moduleService.GetByLearningTrackAsync(track.Id);

        Assert.NotNull(modules);
        Assert.Collection(
            modules,
            module => Assert.Equal(first.Id, module.Id),
            module => Assert.Equal(third.Id, module.Id));
    }
}
