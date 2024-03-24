

using AutoMapper;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HamsterApi.DataAccess.Repositories;

public class TeachingLoadRepository : ITeachingLoadStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    private readonly IMapper _mapper;

    public TeachingLoadRepository(HamsterApiDbContext hamsterApiDbContext,IMapper mapper)
        => (_hamsterApiDbContext,_mapper) = (hamsterApiDbContext,mapper);

    public async Task<string> Create(TeachingLoad item)
    {
        var teachingLoadEntity =_mapper.Map<TeachingLoadEntity>(item);
        await Task.Run(() =>
        {
            _hamsterApiDbContext.TeachingLoadEntities.Add(teachingLoadEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return teachingLoadEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        ITeachingLoadEntity teachingLoadEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            teachingLoadEntity = _hamsterApiDbContext.TeachingLoadEntities.FirstOrDefault(a => a.Id == id)!;
            if (teachingLoadEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(teachingLoadEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<TeachingLoad?> Read(string id)
    {
        ITeachingLoadEntity teachingLoadEntity = null;
        await Task.Run(() =>
        {
            teachingLoadEntity = _hamsterApiDbContext.TeachingLoadEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (teachingLoadEntity is null) return null;

        var teachingLoad = _mapper.Map<TeachingLoad>(teachingLoadEntity);

        return teachingLoad;
    }

    public async Task<List<TeachingLoad>?> ReadAll()
    {
        var teachingLoadEntityList = new List<ITeachingLoadEntity>();
        await Task.Run(() =>
        {
            teachingLoadEntityList = _hamsterApiDbContext.TeachingLoadEntities.ToList();
        }
        );
        var teachingLoadList = teachingLoadEntityList.Select(a => _mapper.Map<TeachingLoad>(a)).ToList();

        return teachingLoadList;
    }

    public async Task<List<TeachingLoad>?> ReadByIds(IEnumerable<string> ids)
    {
        var teachingLoadEntityList = new List<ITeachingLoadEntity>();
        await Task.Run(() =>
        {
            teachingLoadEntityList = _hamsterApiDbContext.TeachingLoadEntities
            .Where(t=>ids.Contains(t.Id))
            .ToList();
        }
        );
        var teachingLoadList = teachingLoadEntityList.Select(a => _mapper.Map<TeachingLoad>(a)).ToList();

        return teachingLoadList;
    }

    public async Task<bool> Update(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax)
    {
        ITeachingLoadEntity teachingLoadEntity = null;
        await Task.Run(() =>
        {
            teachingLoadEntity = _hamsterApiDbContext.TeachingLoadEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (teachingLoadEntity is null) return false;

        teachingLoadEntity.LecturesHours = lecturesHours;
        teachingLoadEntity.PracticeHours = practiceHours;
        teachingLoadEntity.LaboratoryHours = laboratoryHours;
        teachingLoadEntity.LecturesHoursMax = lecturesHoursMax;
        teachingLoadEntity.PracticeHoursMax = practiceHoursMax;
        teachingLoadEntity.LaboratoryHoursMax = laboratoryHoursMax;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
