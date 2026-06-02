using MentorLab.Api.Data;
using MentorLab.Api.Services.Students;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MentorLabDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MentorLab API",
        Version = "v1",
        Description = "API educacional para gestão de mentoria técnica, trilhas de aprendizado, exercícios, entregas e feedbacks."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/api/status", (IHostEnvironment environment) => Results.Ok(new
{
    status = "ok",
    service = "MentorLab API",
    environment = environment.EnvironmentName,
    timestampUtc = DateTimeOffset.UtcNow
}))
.WithName("GetApiStatus")
.WithTags("Status");

app.Run();
