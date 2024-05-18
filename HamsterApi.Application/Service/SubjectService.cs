using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;
internal class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ILogger<SubjectService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public SubjectService(
        ISubjectRepository subjectRepository,
        ILogger<SubjectService> logger,
        IDistributedCache cache)
    {
        _subjectRepository = subjectRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new SubjectConverter() }
        };
    }

    public async Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        _logger.LogInformation($"Adding teachers to subject with id: {id}");
        var result = await _subjectRepository.AddRangeTeacherById(id, teacherId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> AddTeacherById(string id, string teacherId)
    {
        _logger.LogInformation($"Adding teacher {teacherId} to subject with id: {id}");
        var result = await _subjectRepository.AddTeacherById(id, teacherId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<string> Create(Subject item)
    {
        _logger.LogInformation($"Creating subject with id: {item.Id}");
        var result = await _subjectRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created subject with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete subject with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _subjectRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete subject with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted subject with id: {id}");
        }
        return result;
    }

    public async Task<Subject?> Read(string id)
    {
        _logger.LogInformation($"Starting to read subject with id: {id}");
        var subjectString = await _cache.GetStringAsync(id);
        if (subjectString != null)
        {
            var subject = JsonSerializer.Deserialize<Subject>(subjectString, _jsonOptions);
            _logger.LogInformation($"Read subject from cache with id: {id}");
            return subject;
        }

        var retrievedSubject = await _subjectRepository.Read(id);
        if (retrievedSubject == null)
        {
            _logger.LogWarning($"Subject with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedSubject, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read subject from database with id: {id}");
        return retrievedSubject;
    }

    public async Task<List<Subject>> ReadAll()
    {
        _logger.LogInformation("Starting to read all subjects");
        return await _subjectRepository.ReadAll();
    }

    public async Task<List<Subject>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read subjects with ids: {string.Join(',', ids)}");
        return await _subjectRepository.ReadByIds(ids);
    }

    public async Task<Subject?> ReadByIndex(string index)
    {
        _logger.LogInformation($"Starting to read subject with index: {index}");
        var subjectString = await _cache.GetStringAsync(index);
        if (subjectString != null)
        {
            var subject = JsonSerializer.Deserialize<Subject>(subjectString, _jsonOptions);
            _logger.LogInformation($"Read subject from cache with index: {index}");
            return subject;
        }

        var retrievedSubject = await _subjectRepository.ReadByIndex(index);
        if (retrievedSubject == null)
        {
            _logger.LogWarning($"Subject with index: {index} not found");
            return null;
        }

        await _cache.SetStringAsync(index, JsonSerializer.Serialize(retrievedSubject, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read subject from database with index: {index}");
        return retrievedSubject;
    }

    public async Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        _logger.LogInformation($"Removing teachers from subject with id: {id}");
        var result = await _subjectRepository.RemoveRangeTeacherById(id, teacherId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> RemoveTeacherById(string id, string teacherId)
    {
        _logger.LogInformation($"Removing teacher {teacherId} from subject with id: {id}");
        var result = await _subjectRepository.RemoveTeacherById(id, teacherId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> Update(string id, string title, string index, IReadOnlyCollection<string> teachersIds)
    {
        _logger.LogInformation($"Updating subject with id: {id}");
        var result = await _subjectRepository.Update(id, title, index, teachersIds);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached subject with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update subject with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedSubject = await _subjectRepository.Read(id);
        if (updatedSubject != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedSubject, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for subject with id: {id}");
        }
    }
}


