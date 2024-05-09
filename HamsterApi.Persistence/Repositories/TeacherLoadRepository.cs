using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence.MappingExtensions;


namespace HamsterApi.Persistence.Repositories;

internal class TeachingLoadRepository : ITeachingLoadRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public TeachingLoadRepository(HamsterApiDbContext hamsterApiDbContext)
        => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<string> Create(TeachingLoad item)
    {
        var teachingLoadEntity = item.ToEntity();
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

        var teachingLoad = teachingLoadEntity.ToModel();

        return teachingLoad;
    }

    public async Task<List<TeachingLoad>> ReadAll()
    {
        var teachingLoadEntityList = new List<ITeachingLoadEntity>();
        await Task.Run(() =>
        {
            teachingLoadEntityList = _hamsterApiDbContext.TeachingLoadEntities.ToList();
        }
        );
        if (teachingLoadEntityList is null) return [];
        var teachingLoadList = teachingLoadEntityList.Select(a => a.ToModel()).ToList();

        return teachingLoadList;
    }

    public async Task<List<TeachingLoad>> ReadByIds(IEnumerable<string> ids)
    {
        var teachingLoadEntityList = new List<ITeachingLoadEntity>();
        await Task.Run(() =>
        {
            teachingLoadEntityList = _hamsterApiDbContext.TeachingLoadEntities
            .Where(t=>ids.Contains(t.Id))
            .ToList();
        }
        );
        if (teachingLoadEntityList is null) return [];
        var teachingLoadList = teachingLoadEntityList.Select(a => a.ToModel()).ToList();

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
