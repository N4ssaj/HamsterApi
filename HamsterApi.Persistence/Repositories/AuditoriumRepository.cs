using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence.MappingExtensions;
using System.Text.Json;

namespace HamsterApi.Persistence.Repositories;

internal class AuditoriumRepository : IAuditoriumRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public AuditoriumRepository(HamsterApiDbContext hamsterApiDbContext)
        => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<string> Create(Auditorium item)
    {
        var auditoriumEntity = item.ToEnity();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.AuditoriumEntities.Add(auditoriumEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return auditoriumEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        IAuditoriumEntity auditoriumEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            auditoriumEntity = _hamsterApiDbContext.AuditoriumEntities.FirstOrDefault(a => a.Id == id)!;
            if (auditoriumEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(auditoriumEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Auditorium?> Read(string id)
    {
        IAuditoriumEntity auditoriumEntity = null;
        await Task.Run(() =>
        {
            auditoriumEntity = _hamsterApiDbContext.AuditoriumEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (auditoriumEntity is null) return null;

        var auditorium = auditoriumEntity.ToModel();

        return auditorium;
    }

    public async Task<Auditorium?> ReadByNumber(string number)
    {
        IAuditoriumEntity auditoriumEntity = null;
        await Task.Run(() =>
        {
            auditoriumEntity = _hamsterApiDbContext.AuditoriumEntities.FirstOrDefault(a => a.Number == number)!;
        });
        if (auditoriumEntity is null) return null;

        var auditorium = auditoriumEntity.ToModel();

        return auditorium;
    }

    public async Task<List<Auditorium>> ReadAll()
    {
        var auditoriumEntityList = new List<IAuditoriumEntity>();
        await Task.Run(() =>
        {
            auditoriumEntityList = _hamsterApiDbContext.AuditoriumEntities.ToList();
        }
        );
        if (auditoriumEntityList is null) return [];
        var auditoriumList = auditoriumEntityList.Select(a => a.ToModel()).ToList();

        return auditoriumList;
    }

    public async Task<bool> Update(string id, string number)
    {
        IAuditoriumEntity auditoriumEntity = null;
        await Task.Run(() =>
        {
            auditoriumEntity = _hamsterApiDbContext.AuditoriumEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (auditoriumEntity is null) return false;

        auditoriumEntity.Number = number;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }

    public async Task<List<Auditorium>> ReadByIds(IEnumerable<string> ids)
    {
        var auditoriumEntityList = new List<IAuditoriumEntity>();
        await Task.Run(() =>
        {
            auditoriumEntityList = _hamsterApiDbContext.AuditoriumEntities
            .Where(a => ids.Contains(a.Id))
            .ToList();
        }
        );
        if (auditoriumEntityList is null) return [];
        var auditoriumList = auditoriumEntityList.Select(a => a.ToModel()).ToList();

        return auditoriumList;
    }
}