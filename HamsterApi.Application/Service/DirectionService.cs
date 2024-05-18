using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class DirectionService : IDirectionService
{
    private readonly IDirectionRepository _directionRepository;
    private readonly ILogger<DirectionService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public DirectionService(
        IDirectionRepository directionRepository,
        ILogger<DirectionService> logger,
        IDistributedCache cache)
    {
        _directionRepository = directionRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new DirectionConverter() }
        };
    }

    public async Task<bool> AddDepartment(string id, string departmentId)
    {
        _logger.LogInformation($"Adding department {departmentId} to direction with id: {id}");
        var result = await _directionRepository.AddDepartment(id, departmentId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> AddGroupById(string id, string groupId)
    {
        _logger.LogInformation($"Adding group {groupId} to direction with id: {id}");
        var result = await _directionRepository.AddGroupById(id, groupId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> AddRangeGroupById(string id, IEnumerable<string> groupId)
    {
        _logger.LogInformation($"Adding groups to direction with id: {id}");
        var result = await _directionRepository.AddRangeGroupById(id, groupId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<string> Create(Direction item)
    {
        _logger.LogInformation($"Creating direction with id: {item.Id}");
        var result = await _directionRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created direction with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete direction with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _directionRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete direction with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted direction with id: {id}");
        }
        return result;
    }

    public async Task<Direction?> Read(string id)
    {
        _logger.LogInformation($"Starting to read direction with id: {id}");
        var directionString = await _cache.GetStringAsync(id);
        if (directionString != null)
        {
            var direction = JsonSerializer.Deserialize<Direction>(directionString, _jsonOptions);
            _logger.LogInformation($"Read direction from cache with id: {id}");
            return direction;
        }

        var retrievedDirection = await _directionRepository.Read(id);
        if (retrievedDirection == null)
        {
            _logger.LogWarning($"Direction with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedDirection, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read direction from database with id: {id}");
        return retrievedDirection;
    }

    public async Task<List<Direction>> ReadAll()
    {
        _logger.LogInformation("Starting to read all directions");
        return await _directionRepository.ReadAll();
    }

    public async Task<List<Direction>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read directions with ids: {string.Join(',', ids)}");
        return await _directionRepository.ReadByIds(ids);
    }

    public async Task<bool> RemoveDepartment(string id)
    {
        _logger.LogInformation($"Removing department from direction with id: {id}");
        var result = await _directionRepository.RemoveDepartment(id);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> RemoveGroupById(string id, string groupId)
    {
        _logger.LogInformation($"Removing group {groupId} from direction with id: {id}");
        var result = await _directionRepository.RemoveGroupById(id, groupId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> RemoveRangeGroupById(string id, IEnumerable<string> groupId)
    {
        _logger.LogInformation($"Removing groups from direction with id: {id}");
        var result = await _directionRepository.RemoveRangeGroupById(id, groupId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> groupsIds, FormOfEducation formOfEducation, LevelOfEducation levelOfEducation, string departmentId)
    {
        _logger.LogInformation($"Updating direction with id: {id}");
        var result = await _directionRepository.Update(id, title, groupsIds, formOfEducation, levelOfEducation, departmentId);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached direction with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update direction with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedDirection = await _directionRepository.Read(id);
        if (updatedDirection != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedDirection, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for direction with id: {id}");
        }
    }
}


