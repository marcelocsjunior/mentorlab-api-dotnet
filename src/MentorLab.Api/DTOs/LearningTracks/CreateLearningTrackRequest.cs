namespace MentorLab.Api.DTOs.LearningTracks;

public record CreateLearningTrackRequest(
    string Title,
    string? Description
);
