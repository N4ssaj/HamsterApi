using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class TeacherLoadService : ITeacherLoadService
{
    private readonly ITeachingLoadRepository _teacherLoadRepository;
    private readonly ILogger<TeacherLoadService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public TeacherLoadService(
        ITeachingLoadRepository teacherLoadRepository,
        ILogger<TeacherLoadService> logger,
        IDistributedCache cache)
    {
        _teacherLoadRepository = teacherLoadRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new TeachingLoadConverter() }
        };
    }

    public async Task<string> Create(TeachingLoad item)
    {
        _logger.LogInformation($"Creating teaching load with id: {item.Id}");
        var result = await _teacherLoadRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created teaching load with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete teaching load with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _teacherLoadRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete teaching load with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted teaching load with id: {id}");
        }
        return result;
    }

    public async Task<TeachingLoad?> Read(string id)
    {
        _logger.LogInformation($"Starting to read teaching load with id: {id}");
        var teachingLoadString = await _cache.GetStringAsync(id);
        if (teachingLoadString != null)
        {
            var teachingLoad = JsonSerializer.Deserialize<TeachingLoad>(teachingLoadString, _jsonOptions);
            _logger.LogInformation($"Read teaching load from cache with id: {id}");
            return teachingLoad;
        }

        var retrievedTeachingLoad = await _teacherLoadRepository.Read(id);
        if (retrievedTeachingLoad == null)
        {
            _logger.LogWarning($"Teaching load with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedTeachingLoad, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read teaching load from database with id: {id}");
        return retrievedTeachingLoad;
    }

    public async Task<List<TeachingLoad>> ReadAll()
    {
        _logger.LogInformation("Starting to read all teaching loads");
        return await _teacherLoadRepository.ReadAll();
    }

    public async Task<List<TeachingLoad>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read teaching loads with ids: {string.Join(',', ids)}");
        return await _teacherLoadRepository.ReadByIds(ids);
    }

    public async Task<bool> Update(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax)
    {
        _logger.LogInformation($"Updating teaching load with id: {id}");
        var result = await _teacherLoadRepository.Update(id, lecturesHours, practiceHours, laboratoryHours, lecturesHoursMax, practiceHoursMax, laboratoryHoursMax);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached teaching load with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update teaching load with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedTeachingLoad = await _teacherLoadRepository.Read(id);
        if (updatedTeachingLoad != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedTeachingLoad, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for teaching load with id: {id}");
        }
    }
}


