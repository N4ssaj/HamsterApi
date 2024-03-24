using AutoMapper;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HamsterApi.DataAccess.Repositories;

public class AuditoriumRepository : IAuditoriumStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    private readonly IMapper _mapper;

    public AuditoriumRepository(HamsterApiDbContext hamsterApiDbContext, IMapper mapper)
        => (_hamsterApiDbContext, _mapper) = (hamsterApiDbContext, mapper);

    public async Task<string> Create(Auditorium item)
    {
        var auditoriumEntity=_mapper.Map<AuditoriumEntity>(item);
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
            if(auditoriumEntity is not null)
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

        var auditorium = _mapper.Map<Auditorium>(auditoriumEntity);

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

        var auditorium = _mapper.Map<Auditorium>(auditoriumEntity);

        return auditorium;
    }

    public async Task<List<Auditorium>?> ReadAll()
    {
        var auditoriumEntityList = new List<IAuditoriumEntity>();
        await Task.Run(() =>
        {
            auditoriumEntityList = _hamsterApiDbContext.AuditoriumEntities.ToList();
        }
        );
        var auditoriumList = auditoriumEntityList.Select(a => _mapper.Map<Auditorium>(a)).ToList();

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

    public async Task<List<Auditorium>?> ReadByIds(IEnumerable<string> ids)
    {
        var auditoriumEntityList = new List<IAuditoriumEntity>();
        await Task.Run(() =>
        {
            auditoriumEntityList = _hamsterApiDbContext.AuditoriumEntities
            .Where(a=>ids.Contains(a.Id))
            .ToList();
        }
        );
        var auditoriumList = auditoriumEntityList.Select(a => _mapper.Map<Auditorium>(a)).ToList();

        return auditoriumList;
    }
}
