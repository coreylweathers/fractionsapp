using FractionsApp.Shared.Data.Interfaces;
using FractionsApp.Shared.Data.Repositories;
using FractionsApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FractionsApp.Shared.Controllers;

[Route("[controller]")]
[ApiController]
public class ProblemSetsController : ControllerBase
{
    private readonly IProblemSetRepository _repository;
    private readonly ILogger<ProblemSetsController> _logger;

    public ProblemSetsController(IProblemSetRepository repository, ILogger<ProblemSetsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProblemSetModel>>> GetAllProblemSets()
    {
        try
        {
            var problemSets = await _repository.GetAllProblemSetsAsync();
            return Ok(problemSets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all problem sets");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProblemSetModel>> GetProblemSetById(Guid id)
    {
        try
        {
            var problemSet = await _repository.GetProblemSetByIdAsync(id);
            if (problemSet == null)
            {
                return NotFound();
            }
            return Ok(problemSet);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving problem set with ID {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProblemSetModel>> CreateProblemSet(ProblemSetModel problemSet)
    {
        try
        {
            var createdProblemSet = await _repository.CreateProblemSetAsync(problemSet);
            return CreatedAtAction(nameof(GetProblemSetById), new { id = createdProblemSet.Id }, createdProblemSet);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating problem set");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProblemSet(Guid id, ProblemSetModel problemSet)
    {
        if (id != problemSet.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            await _repository.UpdateProblemSetAsync(problemSet);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating problem set with ID {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProblemSet(Guid id)
    {
        try
        {
            await _repository.DeleteProblemSetAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting problem set with ID {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}