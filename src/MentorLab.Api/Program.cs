using MentorLab.Api.Data;
using MentorLab.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "data"));

builder.Services.AddControllers();

builder.Services.AddDbContext<MentorLabDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MentorLabDatabase")
        ?? "Data Source=data/mentorlab.db";

    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<StudentService>();

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MentorLabDbContext>();
    dbContext.Database.EnsureCreated();
}

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
