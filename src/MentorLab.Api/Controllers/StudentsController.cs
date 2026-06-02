using MentorLab.Api.Dtos;
using MentorLab.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MentorLab.Api.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentsController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<StudentResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<StudentResponse>>> GetAll([FromQuery] bool includeInactive = false)
    {
        var students = await _studentService.GetAllAsync(includeInactive);
        return Ok(students);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentResponse>> GetById(Guid id)
    {
        var student = await _studentService.GetByIdAsync(id);

        if (student is null)
        {
            return NotFound(new { message = "Aluno não encontrado." });
        }

        return Ok(student);
    }

    [HttpPost]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<StudentResponse>> Create(CreateStudentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            return BadRequest(new { message = "Nome completo é obrigatório." });
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return BadRequest(new { message = "E-mail é obrigatório." });
        }

        if (await _studentService.EmailExistsAsync(request.Email))
        {
            return Conflict(new { message = "Já existe aluno cadastrado com este e-mail." });
        }

        var student = await _studentService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<StudentResponse>> Update(Guid id, UpdateStudentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            return BadRequest(new { message = "Nome completo é obrigatório." });
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return BadRequest(new { message = "E-mail é obrigatório." });
        }

        if (await _studentService.EmailExistsAsync(request.Email, id))
        {
            return Conflict(new { message = "Já existe outro aluno cadastrado com este e-mail." });
        }

        var student = await _studentService.UpdateAsync(id, request);

        if (student is null)
        {
            return NotFound(new { message = "Aluno não encontrado." });
        }

        return Ok(student);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deactivated = await _studentService.DeactivateAsync(id);

        if (!deactivated)
        {
            return NotFound(new { message = "Aluno não encontrado." });
        }

        return NoContent();
    }
}
