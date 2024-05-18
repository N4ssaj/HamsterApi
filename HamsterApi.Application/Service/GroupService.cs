using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly ILogger<GroupService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public GroupService(
        IGroupRepository groupRepository,
        ILogger<GroupService> logger,
        IDistributedCache cache)
    {
        _groupRepository = groupRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new GroupConverter() }
        };
    }

    public async Task<bool> AddDirection(string id, string directionId)
    {
        _logger.LogInformation($"Adding direction {directionId} to group with id: {id}");
        var result = await _groupRepository.AddDirection(id, directionId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<string> Create(Group item)
    {
        _logger.LogInformation($"Creating group with id: {item.Id}");
        var result = await _groupRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created group with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete group with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _groupRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete group with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted group with id: {id}");
        }
        return result;
    }

    public async Task<Group?> Read(string id)
    {
        _logger.LogInformation($"Starting to read group with id: {id}");
        var groupString = await _cache.GetStringAsync(id);
        if (groupString != null)
        {
            var group = JsonSerializer.Deserialize<Group>(groupString, _jsonOptions);
            _logger.LogInformation($"Read group from cache with id: {id}");
            return group;
        }

        var retrievedGroup = await _groupRepository.Read(id);
        if (retrievedGroup == null)
        {
            _logger.LogWarning($"Group with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedGroup, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read group from database with id: {id}");
        return retrievedGroup;
    }

    public async Task<List<Group>> ReadAll()
    {
        _logger.LogInformation("Starting to read all groups");
        return await _groupRepository.ReadAll();
    }

    public async Task<List<Group>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read groups with ids: {string.Join(',', ids)}");
        return await _groupRepository.ReadByIds(ids);
    }

    public async Task<Group?> ReadByNumber(string number)
    {
        _logger.LogInformation($"Starting to read group with number: {number}");
        var groupString = await _cache.GetStringAsync(number);
        if (groupString != null)
        {
            var group = JsonSerializer.Deserialize<Group>(groupString, _jsonOptions);
            _logger.LogInformation($"Read group from cache with number: {number}");
            return group;
        }

        var retrievedGroup = await _groupRepository.ReadByNumber(number);
        if (retrievedGroup == null)
        {
            _logger.LogWarning($"Group with number: {number} not found");
            return null;
        }

        await _cache.SetStringAsync(number, JsonSerializer.Serialize(retrievedGroup, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read group from database with number: {number}");
        return retrievedGroup;
    }

    public async Task<bool> RemoveDirection(string id)
    {
        _logger.LogInformation($"Removing direction from group with id: {id}");
        var result = await _groupRepository.RemoveDirection(id);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> Update(string id, string number, LevelOfEducation levelOfEducation, string directionId)
    {
        _logger.LogInformation($"Updating group with id: {id}");
        var result = await _groupRepository.Update(id, number, levelOfEducation, directionId);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached group with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update group with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedGroup = await _groupRepository.Read(id);
        if (updatedGroup != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedGroup, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for group with id: {id}");
        }
    }
}
