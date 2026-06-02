using MentorLab.Api.Dtos;

namespace MentorLab.Api.Services.Students;

public interface IStudentService
{
    Task<List<StudentResponse>> GetAllAsync();

    Task<StudentResponse?> GetByIdAsync(int id);

    Task<StudentResponse> CreateAsync(CreateStudentRequest request);

    Task<StudentResponse?> UpdateAsync(int id, UpdateStudentRequest request);

    Task<bool> DeactivateAsync(int id);
}
