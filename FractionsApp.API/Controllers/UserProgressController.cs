using FractionsApp.Shared.Data.Interfaces;
using FractionsApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FractionsApp.Shared.Controllers;

[ApiController]
[Route("[controller]")]
public class UserProgressController : ControllerBase
{
    private readonly IUserProgressRepository _repository;
    private readonly ILogger<UserProgressController> _logger;

    public UserProgressController(IUserProgressRepository repository, ILogger<UserProgressController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<List<UserProgressModel>>> GetUserProgressByUserId(string userId)
    {
        try
        {
            var progressItems = await _repository.GetUserProgressByUserIdAsync(userId);
            return Ok(progressItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user progress for user {UserId}", userId);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("details/{id}")]
    public async Task<ActionResult<UserProgressModel>> GetUserProgressById(Guid id)
    {
        try
        {
            var progress = await _repository.GetUserProgressByIdAsync(id);
            if (progress == null)
            {
                return NotFound();
            }
            return Ok(progress);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user progress with ID {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserProgressModel>> CreateUserProgress(UserProgressModel userProgress)
    {
        try
        {
            var createdProgress = await _repository.CreateUserProgressAsync(userProgress);
            return CreatedAtAction(nameof(GetUserProgressById), new { id = createdProgress.Id }, createdProgress);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user progress");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUserProgress(Guid id, UserProgressModel userProgress)
    {
        if (id != userProgress.Id)
        {
            return BadRequest("ID mismatch");
        }
            
        try
        {
            await _repository.UpdateUserProgressAsync(userProgress);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user progress with ID {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("sync")]
    public async Task<ActionResult<List<UserProgressModel>>> SyncUserProgress(List<UserProgressModel> progressItems)
    {
        try
        {
            var syncedItems = new List<UserProgressModel>();
            foreach (var item in progressItems)
            {
                if (item.Id == Guid.Empty)
                {
                    syncedItems.Add(await _repository.CreateUserProgressAsync(item));
                }
                else
                {
                    await _repository.UpdateUserProgressAsync(item);
                    syncedItems.Add(item);
                }
            }
            return Ok(syncedItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error syncing user progress");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserProgress(Guid id)
    {
        try
        {
            await _repository.DeleteUserProgressAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user progress with ID {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}