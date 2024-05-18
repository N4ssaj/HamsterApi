using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class CurriculumService : ICurriculumService
{
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly ILogger<CurriculumService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public CurriculumService(
        ICurriculumRepository curriculumRepository,
        ILogger<CurriculumService> logger,
        IDistributedCache cache)
    {
        _curriculumRepository = curriculumRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new CurriculumConverter(), new SubjectWtihLoadConverter() }
        };
    }

    public async Task<bool> AddElectives(string id, IEnumerable<SubjectWtihLoad> subjectWtihLoads)
    {
        _logger.LogInformation($"Adding electives to curriculum with id: {id}");
        var result = await _curriculumRepository.AddElectives(id, subjectWtihLoads);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> AddSubject(string id, IEnumerable<SubjectWtihLoad> subjectWtihLoads)
    {
        _logger.LogInformation($"Adding subjects to curriculum with id: {id}");
        var result = await _curriculumRepository.AddSubject(id, subjectWtihLoads);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<string> Create(Curriculum item)
    {
        _logger.LogInformation($"Creating curriculum with id: {item.Id}");
        var result = await _curriculumRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created curriculum with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete curriculum with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _curriculumRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete curriculum with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted curriculum with id: {id}");
        }
        return result;
    }

    public async Task<Curriculum?> Read(string id)
    {
        _logger.LogInformation($"Starting to read curriculum with id: {id}");
        var curriculumString = await _cache.GetStringAsync(id);
        if (curriculumString != null)
        {
            var curriculum = JsonSerializer.Deserialize<Curriculum>(curriculumString, _jsonOptions);
            _logger.LogInformation($"Read curriculum from cache with id: {id}");
            return curriculum;
        }

        var retrievedCurriculum = await _curriculumRepository.Read(id);
        if (retrievedCurriculum == null)
        {
            _logger.LogWarning($"Curriculum with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedCurriculum, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read curriculum from database with id: {id}");
        return retrievedCurriculum;
    }

    public async Task<List<Curriculum>> ReadAll()
    {
        _logger.LogInformation("Starting to read all curriculums");
        return await _curriculumRepository.ReadAll();
    }

    public async Task<List<Curriculum>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read curriculums with ids: {string.Join(',', ids)}");
        return await _curriculumRepository.ReadByIds(ids);
    }

    public async Task<bool> RemoveElectives(string id, IEnumerable<string> subjectIds)
    {
        _logger.LogInformation($"Removing electives from curriculum with id: {id}");
        var result = await _curriculumRepository.RemoveElectives(id, subjectIds);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> RemoveSubject(string id, IEnumerable<string> subjectIds)
    {
        _logger.LogInformation($"Removing subjects from curriculum with id: {id}");
        var result = await _curriculumRepository.RemoveSubject(id, subjectIds);
        if (result)
        {
            await UpdateCache(id);
        }
        return result;
    }

    public async Task<bool> Update(string id, string chairId, string departmentId, string directionId, List<SubjectWtihLoad> curriculumsSubjects, List<SubjectWtihLoad> curriculumsElectiveSubjects, int yearOfPreparation, string fGOSNumber)
    {
        _logger.LogInformation($"Updating curriculum with id: {id}");
        var result = await _curriculumRepository.Update(id, chairId, departmentId, directionId, curriculumsSubjects, curriculumsElectiveSubjects, yearOfPreparation, fGOSNumber);
        if (result)
        {
            await UpdateCache(id);
            _logger.LogInformation($"Updated and cached curriculum with id: {id}");
        }
        else
        {
            _logger.LogWarning($"Failed to update curriculum with id: {id}");
        }
        return result;
    }

    private async Task UpdateCache(string id)
    {
        var updatedCurriculum = await _curriculumRepository.Read(id);
        if (updatedCurriculum != null)
        {
            await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedCurriculum, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
            });
            _logger.LogInformation($"Updated cache for curriculum with id: {id}");
        }
    }
}


