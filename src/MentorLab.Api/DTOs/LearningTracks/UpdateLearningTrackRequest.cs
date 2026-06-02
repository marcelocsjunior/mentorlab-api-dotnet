namespace MentorLab.Api.DTOs.LearningTracks;

public record UpdateLearningTrackRequest(
    string Title,
    string? Description
);
