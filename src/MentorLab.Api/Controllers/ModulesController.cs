using MentorLab.Api.DTOs.Modules;
using MentorLab.Api.Services.Modules;
using Microsoft.AspNetCore.Mvc;

namespace MentorLab.Api.Controllers;

[ApiController]
[Route("api/modules")]
public class ModulesController : ControllerBase
{
    private readonly IModuleService _moduleService;

    public ModulesController(IModuleService moduleService)
    {
        _moduleService = moduleService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ModuleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModuleResponse>> GetById(int id)
    {
        var module = await _moduleService.GetByIdAsync(id);
        return module is null ? NotFound() : Ok(module);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ModuleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModuleResponse>> Update(int id, UpdateModuleRequest request)
    {
        try
        {
            var module = await _moduleService.UpdateAsync(id, request);

            if (module is null)
            {
                return NotFound(new { message = "Módulo não encontrado." });
            }

            return Ok(module);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _moduleService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Módulo não encontrado." });
        }

        return NoContent();
    }
}
