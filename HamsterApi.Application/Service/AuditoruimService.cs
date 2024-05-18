using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class AuditoriumService : IAuditoriumService
{
    private readonly IAuditoriumRepository _auditoriumRepository;
    private readonly ILogger<AuditoriumService> _logger;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public AuditoriumService(
        IAuditoriumRepository auditoriumRepository,
        ILogger<AuditoriumService> logger,
        IDistributedCache cache)
    {
        _auditoriumRepository = auditoriumRepository;
        _logger = logger;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new AuditoriumConverter() }
        };
    }

    public async Task<string> Create(Auditorium item)
    {
        _logger.LogInformation($"Creating auditorium with id: {item.Id}");
        var result = await _auditoriumRepository.Create(item);
        await _cache.SetStringAsync(item.Id, JsonSerializer.Serialize(item, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Created auditorium with id: {item.Id}");
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        _logger.LogInformation($"Starting to delete auditorium with id: {id}");
        await _cache.RemoveAsync(id);
        var result = await _auditoriumRepository.Delete(id);
        if (!result)
        {
            _logger.LogWarning($"Failed to delete auditorium with id: {id}");
        }
        else
        {
            _logger.LogInformation($"Deleted auditorium with id: {id}");
        }
        return result;
    }

    public async Task<Auditorium?> Read(string id)
    {
        _logger.LogInformation($"Starting to read auditorium with id: {id}");
        var auditoriumString = await _cache.GetStringAsync(id);
        if (auditoriumString != null)
        {
            var auditorium = JsonSerializer.Deserialize<Auditorium>(auditoriumString, _jsonOptions);
            _logger.LogInformation($"Read auditorium from cache with id: {id}");
            return auditorium;
        }

        var retrievedAuditorium = await _auditoriumRepository.Read(id);
        if (retrievedAuditorium == null)
        {
            _logger.LogWarning($"Auditorium with id: {id} not found");
            return null;
        }

        await _cache.SetStringAsync(id, JsonSerializer.Serialize(retrievedAuditorium, _jsonOptions), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
        });
        _logger.LogInformation($"Read auditorium from database with id: {id}");
        return retrievedAuditorium;
    }

    public async Task<List<Auditorium>> ReadAll()
    {
        _logger.LogInformation("Starting to read all auditoriums");
        return await _auditoriumRepository.ReadAll();
    }

    public async Task<List<Auditorium>> ReadByIds(IEnumerable<string> ids)
    {
        _logger.LogInformation($"Starting to read auditoriums with ids: {string.Join(',', ids)}");
        return await _auditoriumRepository.ReadByIds(ids);
    }

    public async Task<bool> Update(string id, string number)
    {
        _logger.LogInformation($"Updating auditorium with id: {id}");
        var result = await _auditoriumRepository.Update(id, number);
        if (result)
        {
            var updatedAuditorium = await _auditoriumRepository.Read(id);
            if (updatedAuditorium != null)
            {
                await _cache.SetStringAsync(id, JsonSerializer.Serialize(updatedAuditorium, _jsonOptions), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)
                });
                _logger.LogInformation($"Updated and cached auditorium with id: {id}");
            }
        }
        else
        {
            _logger.LogWarning($"Failed to update auditorium with id: {id}");
        }
        return result;
    }
}

