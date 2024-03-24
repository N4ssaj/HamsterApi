
using AutoMapper;
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HamsterApi.DataAccess.Repositories;

public class AcademicLoadRepository : IAcademicLoadStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;
    private readonly IMapper _mapper;

    public AcademicLoadRepository(HamsterApiDbContext hamsterApiDbContext,IMapper mapper)
        => (_hamsterApiDbContext,_mapper) = (hamsterApiDbContext, mapper);


    public async Task<string> Create(AcademicLoad item)
    {
        var academicLoadEntity = _mapper.Map<AcademicLoadEntity>(item);
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

        var academicLoad = _mapper.Map<AcademicLoad>(academicLoadEntity);

        return academicLoad;
    }

    public async Task<List<AcademicLoad>?> ReadAll()
    {
        var academicLoadEntityList = new List<IAcademicLoadEntity>();
        await Task.Run(() =>
        {
            academicLoadEntityList = _hamsterApiDbContext.AcademicLoadEntities.ToList();
        }
        );
        var academicLoadList = academicLoadEntityList.Select(a => _mapper.Map<AcademicLoad>(a))
            .ToList();

        return academicLoadList;
    }

    public async Task<List<AcademicLoad>?> ReadByIds(IEnumerable<string> ids)
    {
        var academicLoadEntityList = new List<IAcademicLoadEntity>();
        await Task.Run(() =>
        {
            academicLoadEntityList = _hamsterApiDbContext.AcademicLoadEntities.Where(a=>ids.Contains(a.Id)).ToList();
        }
        );
        var academicLoadList = academicLoadEntityList.Select(a => _mapper.Map<AcademicLoad>(a))
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

