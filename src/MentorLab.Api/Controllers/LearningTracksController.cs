using MentorLab.Api.DTOs.LearningTracks;
using MentorLab.Api.DTOs.Modules;
using MentorLab.Api.Services.LearningTracks;
using MentorLab.Api.Services.Modules;
using Microsoft.AspNetCore.Mvc;

namespace MentorLab.Api.Controllers;

[ApiController]
[Route("api/learning-tracks")]
public class LearningTracksController : ControllerBase
{
    private readonly ILearningTrackService _learningTrackService;
    private readonly IModuleService _moduleService;

    public LearningTracksController(
        ILearningTrackService learningTrackService,
        IModuleService moduleService)
    {
        _learningTrackService = learningTrackService;
        _moduleService = moduleService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LearningTrackResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LearningTrackResponse>>> GetAll()
    {
        var tracks = await _learningTrackService.GetAllAsync();
        return Ok(tracks);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LearningTrackWithModulesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LearningTrackWithModulesResponse>> GetById(int id)
    {
        var track = await _learningTrackService.GetByIdAsync(id);
        return track is null ? NotFound() : Ok(track);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LearningTrackResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<LearningTrackResponse>> Create(CreateLearningTrackRequest request)
    {
        try
        {
            var track = await _learningTrackService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = track.Id }, track);
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
    [ProducesResponseType(typeof(LearningTrackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<LearningTrackResponse>> Update(int id, UpdateLearningTrackRequest request)
    {
        try
        {
            var track = await _learningTrackService.UpdateAsync(id, request);

            if (track is null)
            {
                return NotFound(new { message = "Trilha não encontrada." });
            }

            return Ok(track);
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
        var deleted = await _learningTrackService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Trilha não encontrada." });
        }

        return NoContent();
    }

    [HttpGet("{trackId:int}/modules")]
    [ProducesResponseType(typeof(List<ModuleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ModuleResponse>>> GetModulesByTrack(int trackId)
    {
        var modules = await _moduleService.GetByLearningTrackAsync(trackId);

        if (modules is null)
        {
            return NotFound(new { message = "Trilha não encontrada." });
        }

        return Ok(modules);
    }

    [HttpPost("{trackId:int}/modules")]
    [ProducesResponseType(typeof(ModuleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModuleResponse>> CreateModule(int trackId, CreateModuleRequest request)
    {
        try
        {
            var module = await _moduleService.CreateAsync(trackId, request);

            if (module is null)
            {
                return NotFound(new { message = "Trilha não encontrada." });
            }

            return CreatedAtAction(nameof(ModulesController.GetById), "Modules", new { id = module.Id }, module);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }
}
