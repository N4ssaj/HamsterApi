using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HamsterApi.DataAccess.Repositories;

public class AuditoriumRepository : IAuditoriumStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public AuditoriumRepository(HamsterApiDbContext hamsterApiDbContext)
        => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<string> Create(Auditorium item)
    {
        var auditoriumEntity=new AuditoriumEntity()
        { Id = item.Id,Number=item.Number };
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
            auditoriumEntity = _hamsterApiDbContext.AuditoriumEntities.FirstOrDefault(a => a.Id == id);
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
            auditoriumEntity = _hamsterApiDbContext.AuditoriumEntities.FirstOrDefault(a => a.Id == id);
        });
        if (auditoriumEntity is null) return null;

        var auditorium = Auditorium.Create(auditoriumEntity.Id, auditoriumEntity.Number);

        return auditorium.Value;
    }

    public async Task<Auditorium?> ReadByNumber(string number)
    {
        IAuditoriumEntity auditoriumEntity = null;
        await Task.Run(() =>
        {
            auditoriumEntity = _hamsterApiDbContext.AuditoriumEntities.FirstOrDefault(a => a.Number == number);
        });
        if (auditoriumEntity is null) return null;

        var auditorium = Auditorium.Create(auditoriumEntity.Id, auditoriumEntity.Number);

        return auditorium.Value;
    }

    public async Task<List<Auditorium>?> ReadAll()
    {
        var auditoriumEntityList = new List<IAuditoriumEntity>();
        await Task.Run(() =>
        {
            auditoriumEntityList = _hamsterApiDbContext.AuditoriumEntities.ToList();
        }
        );
        var auditoriumList = auditoriumEntityList.Select(a => Auditorium.Create(a.Id, a.Number).Value).ToList();

        return auditoriumList;
    }

    public async Task<bool> Update(string id, string number)
    {
        IAuditoriumEntity auditoriumEntity = null;
        await Task.Run(() =>
        {
            auditoriumEntity = _hamsterApiDbContext.AuditoriumEntities.FirstOrDefault(a => a.Id == id);
        });
        if (auditoriumEntity is null) return false;

        auditoriumEntity.Number = number;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
