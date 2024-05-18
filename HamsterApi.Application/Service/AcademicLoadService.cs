using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class AcademicLoadService : IAcademicLoadService
{
    private readonly IAcademicLoadRepository _academicLoadRepository;
    private readonly ILogger<AcademicLoadService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public AcademicLoadService(
        IAcademicLoadRepository academicLoadRepository,
        ILogger<AcademicLoadService> logger,
        IDistributedCache cache)
    {
        _academicLoadRepository = academicLoadRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new AcademicLoadConverter() }
        };
    }

    public async Task<string> Create(AcademicLoad item)
    {
        _logger.LogInformation($"Creating academic load with id: {item.Id}");
        var result = await _academicLoadRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created academic load with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete academic load with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _academicLoadRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete academic load with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted academic load with id: {id}");
        }
        return result;
    }

    public async Task<AcademicLoad?> Read(string id)
    {
        _logger.LogInformation($"Starting to read academic load with id: {id}");
        var academicLoadString = await _cache.GetStringAsync(id);
        if (academicLoadString != null)
        {
            var academicLoad = JsonSerializer.Deserialize<AcademicLoad>(academicLoadString, _jsonOptions);
            _logger.LogInformation($"Read academic load from cache with id: {id}");
            return academicLoad;
        }

        var retrievedAcademicLoad = await _academicLoadRepository.Read(id);
        if (retrievedAcademicLoad == null)
        {
            _logger.LogWarning($"Academic load with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedAcademicLoad, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read academic load from database with id: {id}");
        return retrievedAcademicLoad;
    }

    public async Task<List<AcademicLoad>> ReadAll()
    {
        _logger.LogInformation("Starting to read all academic loads");
        return await _academicLoadRepository.ReadAll();
    }

    public async Task<List<AcademicLoad>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read academic loads with ids: {string.Join(',', ids)}");
        return await _academicLoadRepository.ReadByIds(ids);
    }

    public async Task<bool> Update(string id, int lectures, int laboratory, int practice, int credits, AcademicEvaluationType academicEvaluationType)
    {
        _logger.LogInformation($"Updating academic load with id: {id}");
        var result = await _academicLoadRepository.Update(id, lectures, laboratory, practice, credits, academicEvaluationType);
        if (result)
        {
            var updatedAcademicLoad = await _academicLoadRepository.Read(id);
            if (updatedAcademicLoad != null)
            {
                await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedAcademicLoad, _jsonOptions), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
                });
                _logger.LogInformation($"Updated and cached academic load with id: {id}");
            }
        }
        else
        {
            _logger.LogWarning($"Failed to update academic load with id: {id}");
        }
        return result;
    }
}


