using FractionsApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FractionsApp.Maui.Services;

public interface IProblemSetService
{
    Task<List<ProblemSetModel>> GetAllProblemSetsAsync();
    Task<ProblemSetModel?> GetProblemSetByIdAsync(int id);
    Task<List<ProblemSetModel>> GetProblemSetsByCategoryAsync(string category);
    Task<List<ProblemSetModel>> GetProblemSetsByDifficultyAsync(string difficulty);
}
    
public class ProblemSetService : IProblemSetService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProblemSetService> _logger;
        
    public ProblemSetService(HttpClient httpClient, ILogger<ProblemSetService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
        
    public async Task<List<ProblemSetModel>> GetAllProblemSetsAsync()
    {
        try
        {
            var problemSets = await _httpClient.GetFromJsonAsync<List<ProblemSetModel>>("api/ProblemSets");
            return problemSets ?? new List<ProblemSetModel>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all problem sets");
            return new List<ProblemSetModel>();
        }
    }
        
    public async Task<ProblemSetModel> GetProblemSetByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ProblemSetModel>($"api/ProblemSets/{id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching problem set with ID {Id}", id);
            return null;
        }
    }
        
    public async Task<List<ProblemSetModel>> GetProblemSetsByCategoryAsync(string category)
    {
        try
        {
            var problemSets = await _httpClient.GetFromJsonAsync<List<ProblemSetModel>>($"api/ProblemSets/category/{category}");
            return problemSets ?? new List<ProblemSetModel>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching problem sets for category {Category}", category);
            return new List<ProblemSetModel>();
        }
    }
        
    public async Task<List<ProblemSetModel>> GetProblemSetsByDifficultyAsync(string difficulty)
    {
        try
        {
            var problemSets = await _httpClient.GetFromJsonAsync<List<ProblemSetModel>>($"api/ProblemSets/difficulty/{difficulty}");
            return problemSets ?? new List<ProblemSetModel>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching problem sets for difficulty {Difficulty}", difficulty);
            return new List<ProblemSetModel>();
        }
    }
}