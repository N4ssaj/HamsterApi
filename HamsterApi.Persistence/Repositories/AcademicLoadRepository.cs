using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence.MappingExtensions;


namespace HamsterApi.Persistence.Repositories;

internal class AcademicLoadRepository : IAcademicLoadRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public AcademicLoadRepository(HamsterApiDbContext hamsterApiDbContext)
        => _hamsterApiDbContext = hamsterApiDbContext;


    public async Task<string> Create(AcademicLoad item)
    {
        var academicLoadEntity = item.ToEntity();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.AcademicLoadEntities.Add(academicLoadEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return academicLoadEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        IAcademicLoadEntity academicLoadEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            academicLoadEntity = _hamsterApiDbContext.AcademicLoadEntities.FirstOrDefault(a => a.Id == id)!;
            if (academicLoadEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(academicLoadEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<AcademicLoad?> Read(string id)
    {
        IAcademicLoadEntity academicLoadEntity = null;
        await Task.Run(() =>
        {
            academicLoadEntity = _hamsterApiDbContext.AcademicLoadEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (academicLoadEntity is null) return null;

        var academicLoad = academicLoadEntity.ToModel();

        return academicLoad;
    }

    public async Task<List<AcademicLoad>> ReadAll()
    {
        var academicLoadEntityList = new List<IAcademicLoadEntity>();
        await Task.Run(() =>
        {
            academicLoadEntityList = _hamsterApiDbContext.AcademicLoadEntities.ToList();
        }
        );
        if (academicLoadEntityList is null)
            return [];
        var academicLoadList = academicLoadEntityList.Select(a =>a.ToModel())
            .ToList();


        return academicLoadList;
    }

    public async Task<List<AcademicLoad>> ReadByIds(IEnumerable<string> ids)
    {
        var academicLoadEntityList = new List<IAcademicLoadEntity>();
        await Task.Run(() =>
        {
            academicLoadEntityList = _hamsterApiDbContext.AcademicLoadEntities.Where(a=>ids.Contains(a.Id)).ToList();
        }
        );
        if (academicLoadEntityList is null)
            return [];
        var academicLoadList = academicLoadEntityList.Select(a => a.ToModel())
            .ToList();

        return academicLoadList;
    }

    public async Task<bool> Update(string id, int lectures, int laboratory, int practice, int credits, AcademicEvaluationType academicEvaluationType)
    {
        IAcademicLoadEntity academicLoadEntity = null;
        await Task.Run(() =>
        {
            academicLoadEntity = _hamsterApiDbContext.AcademicLoadEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (academicLoadEntity is null) return false;

        academicLoadEntity.Lectures = lectures;
        academicLoadEntity.Laboratory = laboratory;
        academicLoadEntity.Credits = credits;
        academicLoadEntity.Practice = practice;
        academicLoadEntity.Total = lectures + laboratory + practice + credits;
        academicLoadEntity.AcademicEvaluationType = academicEvaluationType;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}

