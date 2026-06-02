using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using MentorLab.Api.DTOs.LearningTracks;
using MentorLab.Api.DTOs.Modules;
using MentorLab.Api.DTOs.Students;
using MentorLab.Api.Tests.Infrastructure;

namespace MentorLab.Api.Tests.Endpoints;

public class ApiEndpointTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [Fact]
    public async Task GetStatus_ShouldReturnOk()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/status");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetStudents_ShouldReturnOk()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/students");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostStudents_ShouldReturnCreatedForValidPayload()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/students", new CreateStudentRequest(
            "Aluno Endpoint",
            "aluno.endpoint@example.com"));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetLearningTracks_ShouldReturnOk()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/learning-tracks");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostLearningTracks_ShouldReturnCreated()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/learning-tracks", new CreateLearningTrackRequest(
            "Trilha Endpoint",
            "Criada por teste de endpoint."));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task LearningTrackModulesEndpoints_ShouldCreateListAndReadModule()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();
        var track = await CreateTrackAsync(client);

        var createModuleResponse = await client.PostAsJsonAsync(
            $"/api/learning-tracks/{track.Id}/modules",
            new CreateModuleRequest(
                "Controller, DTO e Service",
                "Separação de responsabilidades.",
                1));

        Assert.Equal(HttpStatusCode.Created, createModuleResponse.StatusCode);

        var module = await ReadJsonAsync<ModuleResponse>(createModuleResponse);

        var listModulesResponse = await client.GetAsync($"/api/learning-tracks/{track.Id}/modules");
        var getModuleResponse = await client.GetAsync($"/api/modules/{module.Id}");

        Assert.Equal(HttpStatusCode.OK, listModulesResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getModuleResponse.StatusCode);
    }

    private static async Task<LearningTrackResponse> CreateTrackAsync(HttpClient client)
    {
        var response = await client.PostAsJsonAsync("/api/learning-tracks", new CreateLearningTrackRequest(
            "Fundamentos de ASP.NET Core",
            "Trilha para construção de APIs REST com .NET."));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        return await ReadJsonAsync<LearningTrackResponse>(response);
    }

    private static async Task<T> ReadJsonAsync<T>(HttpResponseMessage response)
    {
        var value = await response.Content.ReadFromJsonAsync<T>(JsonOptions);

        Assert.NotNull(value);
        return value;
    }
}
