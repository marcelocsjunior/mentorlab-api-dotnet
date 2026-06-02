using MentorLab.Api.DTOs.LearningTracks;
using MentorLab.Api.DTOs.Modules;
using MentorLab.Api.Tests.Infrastructure;
using MentorLab.Api.Services.LearningTracks;
using MentorLab.Api.Services.Modules;

namespace MentorLab.Api.Tests.Services;

public class LearningTrackServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateValidLearningTrack()
    {
        using var database = new SqliteTestDatabase();
        var service = new LearningTrackService(database.Context);

        var track = await service.CreateAsync(new CreateLearningTrackRequest(
            "Back-end .NET",
            "Trilha de APIs com ASP.NET Core."));

        Assert.True(track.Id > 0);
        Assert.Equal("Back-end .NET", track.Title);
        Assert.True(track.IsActive);
    }

    [Fact]
    public async Task CreateAsync_ShouldBlockDuplicateActiveTitle()
    {
        using var database = new SqliteTestDatabase();
        var service = new LearningTrackService(database.Context);

        await service.CreateAsync(new CreateLearningTrackRequest(
            "APIs REST",
            "Primeira trilha."));

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.CreateAsync(new CreateLearningTrackRequest(
                "APIs REST",
                "Título duplicado.")));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTrackWithActiveModulesOrdered()
    {
        using var database = new SqliteTestDatabase();
        var trackService = new LearningTrackService(database.Context);
        var moduleService = new ModuleService(database.Context);
        var track = await trackService.CreateAsync(new CreateLearningTrackRequest(
            "Arquitetura de APIs",
            null));

        var second = await moduleService.CreateAsync(track.Id, new CreateModuleRequest(
            "Services",
            null,
            2));
        var first = await moduleService.CreateAsync(track.Id, new CreateModuleRequest(
            "Controllers",
            null,
            1));
        var inactive = await moduleService.CreateAsync(track.Id, new CreateModuleRequest(
            "Removido",
            null,
            3));

        Assert.NotNull(second);
        Assert.NotNull(first);
        Assert.NotNull(inactive);
        await moduleService.DeleteAsync(inactive.Id);

        var result = await trackService.GetByIdAsync(track.Id);

        Assert.NotNull(result);
        Assert.Collection(
            result.Modules,
            module => Assert.Equal(first.Id, module.Id),
            module => Assert.Equal(second.Id, module.Id));
    }

    [Fact]
    public async Task DeleteAsync_ShouldApplySoftDeleteToTrackAndActiveModules()
    {
        using var database = new SqliteTestDatabase();
        var trackService = new LearningTrackService(database.Context);
        var moduleService = new ModuleService(database.Context);
        var track = await trackService.CreateAsync(new CreateLearningTrackRequest(
            "Soft Delete",
            null));
        var module = await moduleService.CreateAsync(track.Id, new CreateModuleRequest(
            "Módulo ativo",
            null,
            1));

        Assert.NotNull(module);
        var deleted = await trackService.DeleteAsync(track.Id);
        var hiddenTrack = await trackService.GetByIdAsync(track.Id);
        var hiddenModule = await moduleService.GetByIdAsync(module.Id);

        Assert.True(deleted);
        Assert.Null(hiddenTrack);
        Assert.Null(hiddenModule);
    }
}
