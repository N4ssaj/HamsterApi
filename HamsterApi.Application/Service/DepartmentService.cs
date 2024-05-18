using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ILogger<DepartmentService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public DepartmentService(
        IDepartmentRepository departmentRepository,
        ILogger<DepartmentService> logger,
        IDistributedCache cache)
    {
        _departmentRepository = departmentRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new DepartmentConverter() }
        };
    }

    public async Task<bool> AddRangeChairById(string id, IEnumerable<string> chairId)
    {
        _logger.LogInformation($"Adding chairs to department with id: {id}");
        var result = await _departmentRepository.AddRangeChairById(id, chairId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> AddRangeDirectionById(string id, IEnumerable<string> directionId)
    {
        _logger.LogInformation($"Adding directions to department with id: {id}");
        var result = await _departmentRepository.AddRangeDirectionById(id, directionId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<string> Create(Department item)
    {
        _logger.LogInformation($"Creating department with id: {item.Id}");
        var result = await _departmentRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created department with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete department with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _departmentRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete department with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted department with id: {id}");
        }
        return result;
    }

    public async Task<Department?> Read(string id)
    {
        _logger.LogInformation($"Starting to read department with id: {id}");
        var departmentString = await _cache.GetStringAsync(id);
        if (departmentString != null)
        {
            var department = JsonSerializer.Deserialize<Department>(departmentString, _jsonOptions);
            _logger.LogInformation($"Read department from cache with id: {id}");
            return department;
        }

        var retrievedDepartment = await _departmentRepository.Read(id);
        if (retrievedDepartment == null)
        {
            _logger.LogWarning($"Department with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedDepartment, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read department from database with id: {id}");
        return retrievedDepartment;
    }

    public async Task<List<Department>> ReadAll()
    {
        _logger.LogInformation("Starting to read all departments");
        return await _departmentRepository.ReadAll();
    }

    public async Task<List<Department>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read departments with ids: {string.Join(',', ids)}");
        return await _departmentRepository.ReadByIds(ids);
    }

    public async Task<bool> RemoveRangeChairById(string id, IEnumerable<string> chairId)
    {
        _logger.LogInformation($"Removing chairs from department with id: {id}");
        var result = await _departmentRepository.RemoveRangeChairById(id, chairId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> RemoveRangeDirectionById(string id, IEnumerable<string> directionId)
    {
        _logger.LogInformation($"Removing directions from department with id: {id}");
        var result = await _departmentRepository.RemoveRangeDirectionById(id, directionId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> chairsIds, IReadOnlyCollection<string> directionsIds)
    {
        _logger.LogInformation($"Updating department with id: {id}");
        var result = await _departmentRepository.Update(id, title, chairsIds, directionsIds);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached department with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update department with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedDepartment = await _departmentRepository.Read(id);
        if (updatedDepartment != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedDepartment, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for department with id: {id}");
        }
    }
}

