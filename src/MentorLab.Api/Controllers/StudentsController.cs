using MentorLab.Api.DTOs.Students;
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
    [ProducesResponseType(typeof(List<StudentResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<StudentResponse>>> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<StudentResponse>> Update(int id, UpdateStudentRequest request)
    {
        try
        {
            var student = await _studentService.UpdateAsync(id, request);

            if (student is null)
            {
                return NotFound(new { message = "Aluno não encontrado." });
            }

            return Ok(student);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _studentService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Aluno não encontrado." });
        }

        return NoContent();
    }
}
