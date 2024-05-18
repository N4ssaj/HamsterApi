using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ILogger<TeacherService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public TeacherService(
        ITeacherRepository teacherRepository,
        ILogger<TeacherService> logger,
        IDistributedCache cache)
    {
        _teacherRepository = teacherRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new TeacherConverter() }
        };
    }

    public async Task<bool> AddChair(string id, string chairId)
    {
        _logger.LogInformation($"Adding chair {chairId} to teacher with id: {id}");
        var result = await _teacherRepository.AddChair(id, chairId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> AddRangeSubjectById(string id, IEnumerable<string> subjectId)
    {
        _logger.LogInformation($"Adding subjects to teacher with id: {id}");
        var result = await _teacherRepository.AddRangeSubjectById(id, subjectId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> AddSubjectById(string id, string subjectId)
    {
        _logger.LogInformation($"Adding subject {subjectId} to teacher with id: {id}");
        var result = await _teacherRepository.AddSubjectById(id, subjectId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<string> Create(Teacher item)
    {
        _logger.LogInformation($"Creating teacher with id: {item.Id}");
        var result = await _teacherRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created teacher with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete teacher with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _teacherRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete teacher with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted teacher with id: {id}");
        }
        return result;
    }

    public async Task<Teacher?> Read(string id)
    {
        _logger.LogInformation($"Starting to read teacher with id: {id}");
        var teacherString = await _cache.GetStringAsync(id);
        if (teacherString != null)
        {
            var teacher = JsonSerializer.Deserialize<Teacher>(teacherString, _jsonOptions);
            _logger.LogInformation($"Read teacher from cache with id: {id}");
            return teacher;
        }

        var retrievedTeacher = await _teacherRepository.Read(id);
        if (retrievedTeacher == null)
        {
            _logger.LogWarning($"Teacher with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedTeacher, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read teacher from database with id: {id}");
        return retrievedTeacher;
    }

    public async Task<List<Teacher>> ReadAll()
    {
        _logger.LogInformation("Starting to read all teachers");
        return await _teacherRepository.ReadAll();
    }

    public async Task<List<Teacher>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read teachers with ids: {string.Join(',', ids)}");
        return await _teacherRepository.ReadByIds(ids);
    }

    public async Task<bool> RemoveChair(string id)
    {
        _logger.LogInformation($"Removing chair from teacher with id: {id}");
        var result = await _teacherRepository.RemoveChair(id);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> RemoveRangeSubjectById(string id, IEnumerable<string> subjectId)
    {
        _logger.LogInformation($"Removing subjects from teacher with id: {id}");
        var result = await _teacherRepository.RemoveRangeSubjectById(id, subjectId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> RemoveSubjectById(string id, string subjectId)
    {
        _logger.LogInformation($"Removing subject {subjectId} from teacher with id: {id}");
        var result = await _teacherRepository.RemoveSubjectById(id, subjectId);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> Update(string id, string name, string surname, string patronymic, IReadOnlyCollection<string> subjectsIds, string chairId, string teacherLoadId)
    {
        _logger.LogInformation($"Updating teacher with id: {id}");
        var result = await _teacherRepository.Update(id, name, surname, patronymic, subjectsIds, chairId, teacherLoadId);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached teacher with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update teacher with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedTeacher = await _teacherRepository.Read(id);
        if (updatedTeacher != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedTeacher, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for teacher with id: {id}");
        }
    }
}


