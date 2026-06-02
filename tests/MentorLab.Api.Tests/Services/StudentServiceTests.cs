using MentorLab.Api.DTOs.Students;
using MentorLab.Api.Tests.Infrastructure;
using MentorLab.Api.Services.Students;

namespace MentorLab.Api.Tests.Services;

public class StudentServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateValidStudent()
    {
        using var database = new SqliteTestDatabase();
        var service = new StudentService(database.Context);

        var student = await service.CreateAsync(new CreateStudentRequest(
            "Ana Silva",
            "ana.silva@example.com"));

        Assert.True(student.Id > 0);
        Assert.Equal("Ana Silva", student.FullName);
        Assert.Equal("ana.silva@example.com", student.Email);
        Assert.True(student.IsActive);
    }

    [Fact]
    public async Task CreateAsync_ShouldNormalizeEmailToLowercase()
    {
        using var database = new SqliteTestDatabase();
        var service = new StudentService(database.Context);

        var student = await service.CreateAsync(new CreateStudentRequest(
            "Bruno Costa",
            "  BRUNO.COSTA@EXAMPLE.COM  "));

        Assert.Equal("bruno.costa@example.com", student.Email);
    }

    [Fact]
    public async Task CreateAsync_ShouldBlockDuplicateActiveEmail()
    {
        using var database = new SqliteTestDatabase();
        var service = new StudentService(database.Context);

        await service.CreateAsync(new CreateStudentRequest(
            "Carla Souza",
            "carla.souza@example.com"));

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.CreateAsync(new CreateStudentRequest(
                "Carla Duplicada",
                "CARLA.SOUZA@example.com")));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingStudent()
    {
        using var database = new SqliteTestDatabase();
        var service = new StudentService(database.Context);
        var student = await service.CreateAsync(new CreateStudentRequest(
            "Diego Lima",
            "diego.lima@example.com"));

        var updated = await service.UpdateAsync(student.Id, new UpdateStudentRequest(
            "Diego Lima Atualizado",
            "DIEGO.ATUALIZADO@EXAMPLE.COM"));

        Assert.NotNull(updated);
        Assert.Equal("Diego Lima Atualizado", updated.FullName);
        Assert.Equal("diego.atualizado@example.com", updated.Email);
        Assert.NotNull(updated.UpdatedAt);
    }

    [Fact]
    public async Task DeleteAsync_ShouldApplySoftDelete()
    {
        using var database = new SqliteTestDatabase();
        var service = new StudentService(database.Context);
        var student = await service.CreateAsync(new CreateStudentRequest(
            "Erica Martins",
            "erica.martins@example.com"));

        var deleted = await service.DeleteAsync(student.Id);
        var hidden = await service.GetByIdAsync(student.Id);

        Assert.True(deleted);
        Assert.Null(hidden);
    }

    [Fact]
    public async Task GetAllAndGetById_ShouldHideInactiveStudent()
    {
        using var database = new SqliteTestDatabase();
        var service = new StudentService(database.Context);
        var active = await service.CreateAsync(new CreateStudentRequest(
            "Fernanda Rocha",
            "fernanda.rocha@example.com"));
        var inactive = await service.CreateAsync(new CreateStudentRequest(
            "Gabriel Alves",
            "gabriel.alves@example.com"));

        await service.DeleteAsync(inactive.Id);

        var students = await service.GetAllAsync();
        var activeById = await service.GetByIdAsync(active.Id);
        var inactiveById = await service.GetByIdAsync(inactive.Id);

        Assert.Single(students);
        Assert.Equal(active.Id, students[0].Id);
        Assert.NotNull(activeById);
        Assert.Null(inactiveById);
    }
}
