var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/api/status", (IHostEnvironment environment) => Results.Ok(new
{
    status = "ok",
    service = "MentorLab API",
    environment = environment.EnvironmentName,
    timestampUtc = DateTimeOffset.UtcNow
}))
.WithName("GetApiStatus")
.WithTags("Status")
.WithOpenApi();

app.Run();
