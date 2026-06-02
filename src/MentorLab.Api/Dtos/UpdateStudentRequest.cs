namespace MentorLab.Api.Dtos;

public sealed class UpdateStudentRequest
{
    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;
}
