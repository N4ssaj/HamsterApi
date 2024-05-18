
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class SemesterService : ISemesterService
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly ILogger<SemesterService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public SemesterService(
        ISemesterRepository semesterRepository,
        ILogger<SemesterService> logger,
        IDistributedCache cache)
    {
        _semesterRepository = semesterRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new SemesterConverter(), new SubjectWtihLoadConverter() }
        };
    }

    public async Task<bool> AddSubjects(string id, IEnumerable<SubjectWtihLoad> subjects)
    {
        _logger.LogInformation($"Adding subjects to semester with id: {id}");
        var result = await _semesterRepository.AddSubjects(id, subjects);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<string> Create(Semester item)
    {
        _logger.LogInformation($"Creating semester with id: {item.Id}");
        var result = await _semesterRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created semester with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete semester with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _semesterRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete semester with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted semester with id: {id}");
        }
        return result;
    }

    public async Task<Semester?> Read(string id)
    {
        _logger.LogInformation($"Starting to read semester with id: {id}");
        var semesterString = await _cache.GetStringAsync(id);
        if (semesterString != null)
        {
            var semester = JsonSerializer.Deserialize<Semester>(semesterString, _jsonOptions);
            _logger.LogInformation($"Read semester from cache with id: {id}");
            return semester;
        }

        var retrievedSemester = await _semesterRepository.Read(id);
        if (retrievedSemester == null)
        {
            _logger.LogWarning($"Semester with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedSemester, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read semester from database with id: {id}");
        return retrievedSemester;
    }

    public async Task<List<Semester>> ReadAll()
    {
        _logger.LogInformation("Starting to read all semesters");
        return await _semesterRepository.ReadAll();
    }

    public async Task<List<Semester>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read semesters with ids: {string.Join(',', ids)}");
        return await _semesterRepository.ReadByIds(ids);
    }

    public async Task<bool> RemoveSubjects(string id, IEnumerable<string> subjectsIds)
    {
        _logger.LogInformation($"Removing subjects from semester with id: {id}");
        var result = await _semesterRepository.RemoveSubjects(id, subjectsIds);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> Update(string id, int number, string groupId, List<SubjectWtihLoad> subjects)
    {
        _logger.LogInformation($"Updating semester with id: {id}");
        var result = await _semesterRepository.Update(id, number, groupId, subjects);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached semester with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update semester with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedSemester = await _semesterRepository.Read(id);
        if (updatedSemester != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedSemester, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for semester with id: {id}");
        }
    }
}

