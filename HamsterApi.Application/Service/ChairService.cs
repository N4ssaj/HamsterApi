using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class ChairService : IChairService
{
    private readonly IChairRepository _chairRepository;
    private readonly ILogger<ChairService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public ChairService(
        IChairRepository chairRepository,
        ILogger<ChairService> logger,
        IDistributedCache cache)
    {
        _chairRepository = chairRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new ChairConverter() }
        };
    }

    public async Task<bool> AddDepartment(string id, string departmentId)
    {
        _logger.LogInformation($"Adding department {departmentId} to chair with id: {id}");
        var result = await _chairRepository.AddDepartment(id, departmentId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        _logger.LogInformation($"Adding teachers to chair with id: {id}");
        var result = await _chairRepository.AddRangeTeacherById(id, teacherId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> AddTeacherById(string id, string teacherId)
    {
        _logger.LogInformation($"Adding teacher {teacherId} to chair with id: {id}");
        var result = await _chairRepository.AddTeacherById(id, teacherId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<string> Create(Chair item)
    {
        _logger.LogInformation($"Creating chair with id: {item.Id}");
        var result = await _chairRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created chair with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete chair with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _chairRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete chair with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted chair with id: {id}");
        }
        return result;
    }

    public async Task<Chair?> Read(string id)
    {
        _logger.LogInformation($"Starting to read chair with id: {id}");
        var chairString = await _cache.GetStringAsync(id);
        if (chairString != null)
        {
            var chair = JsonSerializer.Deserialize<Chair>(chairString, _jsonOptions);
            _logger.LogInformation($"Read chair from cache with id: {id}");
            return chair;
        }

        var retrievedChair = await _chairRepository.Read(id);
        if (retrievedChair == null)
        {
            _logger.LogWarning($"Chair with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedChair, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read chair from database with id: {id}");
        return retrievedChair;
    }

    public async Task<List<Chair>> ReadAll()
    {
        _logger.LogInformation("Starting to read all chairs");
        return await _chairRepository.ReadAll();
    }

    public async Task<List<Chair>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read chairs with ids: {string.Join(',', ids)}");
        return await _chairRepository.ReadByIds(ids);
    }

    public async Task<bool> RemoveDepartment(string id)
    {
        _logger.LogInformation($"Removing department from chair with id: {id}");
        var result = await _chairRepository.RemoveDepartment(id);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        _logger.LogInformation($"Removing teachers from chair with id: {id}");
        var result = await _chairRepository.RemoveRangeTeacherById(id, teacherId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> RemoveTeacherById(string id, string teacherId)
    {
        _logger.LogInformation($"Removing teacher {teacherId} from chair with id: {id}");
        var result = await _chairRepository.RemoveTeacherById(id, teacherId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> teachersIds, string departmentId)
    {
        _logger.LogInformation($"Updating chair with id: {id}");
        var result = await _chairRepository.Update(id, title, teachersIds, departmentId);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached chair with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update chair with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedChair = await _chairRepository.Read(id);
        if (updatedChair != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedChair, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for chair with id: {id}");
        }
    }
}


