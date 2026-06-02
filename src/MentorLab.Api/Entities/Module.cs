namespace MentorLab.Api.Entities;

public class Module
{
    public int Id { get; set; }

    public int LearningTrackId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Order { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public LearningTrack? LearningTrack { get; set; }
}
