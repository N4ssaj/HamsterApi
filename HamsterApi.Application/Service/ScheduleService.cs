using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.Common.Enum;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly ILogger<ScheduleService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public ScheduleService(
        IScheduleRepository scheduleRepository,
        ILogger<ScheduleService> logger,
        IDistributedCache cache)
    {
        _scheduleRepository = scheduleRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new ScheduleConverter() }
        };
    }

    public async Task<bool> AddGroup(string id, IEnumerable<string> groupIds)
    {
        _logger.LogInformation($"Adding groups to schedule with id: {id}");
        var result = await _scheduleRepository.AddGroup(id, groupIds);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<string> Create(Schedule item)
    {
        _logger.LogInformation($"Creating schedule with id: {item.Id}");
        var result = await _scheduleRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created schedule with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete schedule with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _scheduleRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete schedule with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted schedule with id: {id}");
        }
        return result;
    }

    public async Task<Schedule?> Read(string id)
    {
        _logger.LogInformation($"Starting to read schedule with id: {id}");
        var scheduleString = await _cache.GetStringAsync(id);
        if (scheduleString != null)
        {
            var schedule = JsonSerializer.Deserialize<Schedule>(scheduleString, _jsonOptions);
            _logger.LogInformation($"Read schedule from cache with id: {id}");
            return schedule;
        }

        var retrievedSchedule = await _scheduleRepository.Read(id);
        if (retrievedSchedule == null)
        {
            _logger.LogWarning($"Schedule with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedSchedule, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read schedule from database with id: {id}");
        return retrievedSchedule;
    }

    public async Task<List<Schedule>> ReadAll()
    {
        _logger.LogInformation("Starting to read all schedules");
        return await _scheduleRepository.ReadAll();
    }

    public async Task<List<Schedule>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read schedules with ids: {string.Join(',', ids)}");
        return await _scheduleRepository.ReadByIds(ids);
    }

    public async Task<bool> RemoveGroup(string id, IEnumerable<string> groupIds)
    {
        _logger.LogInformation($"Removing groups from schedule with id: {id}");
        var result = await _scheduleRepository.RemoveGroup(id, groupIds);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> Update(string id, int year, SpringOrAutumn springOrAutumn, IReadOnlyCollection<string> groupsScheduleIds)
    {
        _logger.LogInformation($"Updating schedule with id: {id}");
        var result = await _scheduleRepository.Update(id, year, springOrAutumn, groupsScheduleIds);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached schedule with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update schedule with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedSchedule = await _scheduleRepository.Read(id);
        if (updatedSchedule != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedSchedule, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for schedule with id: {id}");
        }
    }
}
