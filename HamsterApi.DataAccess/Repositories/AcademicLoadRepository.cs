
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HamsterApi.DataAccess.Repositories;

public class AcademicLoadRepository : IAcademicLoadStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public AcademicLoadRepository(HamsterApiDbContext hamsterApiDbContext)
    {
        _hamsterApiDbContext = hamsterApiDbContext;
    }

    public async Task<string> Create(AcademicLoad item)
    {
        var academicLoadEntity = new AcademicLoadEntity()
        { Id = item.Id,
         AcademicEvaluationType=item.AcademicEvaluationType,
         Credits=item.Credits,
         Laboratory=item.Laboratory,
         Lectures = item.Lectures,
         Practice = item.Practice,
         Total = item.Total
        };
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
            academicLoadEntity = _hamsterApiDbContext.AcademicLoadEntities.FirstOrDefault(a => a.Id == id);
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
            academicLoadEntity = _hamsterApiDbContext.AcademicLoadEntities.FirstOrDefault(a => a.Id == id);
        });
        if (academicLoadEntity is null) return null;

        var academicLoad = AcademicLoad.Create(academicLoadEntity.Id, academicLoadEntity.Lectures, academicLoadEntity.Laboratory,
            academicLoadEntity.Practice, academicLoadEntity.Credits, academicLoadEntity.AcademicEvaluationType);

        return academicLoad.Value;
    }

    public async Task<List<AcademicLoad>?> ReadAll()
    {
        var academicLoadEntityList = new List<IAcademicLoadEntity>();
        await Task.Run(() =>
        {
            academicLoadEntityList = _hamsterApiDbContext.AcademicLoadEntities.ToList();
        }
        );
        var academicLoadList = academicLoadEntityList.Select(a => AcademicLoad.Create(a.Id, a.Lectures, a.Laboratory,
            a.Practice, a.Credits, a.AcademicEvaluationType).Value).ToList();

        return academicLoadList;
    }

    public async Task<bool> Update(string id, int lectures, int laboratory, int practice, int credits, AcademicEvaluationType academicEvaluationType)
    {
        IAcademicLoadEntity academicLoadEntity = null;
        await Task.Run(() =>
        {
            academicLoadEntity = _hamsterApiDbContext.AcademicLoadEntities.FirstOrDefault(a => a.Id == id);
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

