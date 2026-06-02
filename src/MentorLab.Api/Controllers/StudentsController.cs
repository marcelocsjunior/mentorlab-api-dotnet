using MentorLab.Api.Dtos;
using MentorLab.Api.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace MentorLab.Api.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentResponse>>> GetAll()
    {
        return Ok(await _studentService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentResponse>> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        return student is null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<StudentResponse>> Create(CreateStudentRequest request)
    {
        try
        {
            var student = await _studentService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<StudentResponse>> Update(int id, UpdateStudentRequest request)
    {
        try
        {
            var student = await _studentService.UpdateAsync(id, request);
            return student is null ? NotFound() : Ok(student);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deactivated = await _studentService.DeactivateAsync(id);
        return deactivated ? NoContent() : NotFound();
    }
}
