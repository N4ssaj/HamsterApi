
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using AutoMapper;

namespace HamsterApi.DataAccess.Repositories;

public class SubjectRepository : ISubjectStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;
    private readonly IMapper _mapper;

    public SubjectRepository(HamsterApiDbContext hamsterApiDbContext, IMapper mapper)
            => (_hamsterApiDbContext,_mapper) = (hamsterApiDbContext,mapper);

    public async Task<string> Create(Subject item)
    {
        var subjectEntity = _mapper.Map<SubjectEntity>(item);
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SubjectEntities.Add(subjectEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return subjectEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        ISubjectEntity subjectEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Id == id)!;
            if (subjectEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(subjectEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Subject?> Read(string id)
    {
        ISubjectEntity subjectEntity = null;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (subjectEntity is null) return null;

        var subject = _mapper.Map<Subject>(subjectEntity);

        return subject;
    }

    public async Task<List<Subject>?> ReadAll()
    {
        var subjectEntityList = new List<ISubjectEntity>();
        await Task.Run(() =>
        {
            subjectEntityList = _hamsterApiDbContext.SubjectEntities.ToList();
        }
        );
        var subjectList = subjectEntityList.Select(a =>_mapper.Map<Subject>(a)).ToList();

        return subjectList;
    }

    public Task<List<Subject>?> ReadByIds(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public async Task<Subject?> ReadByIndex(string index)
    {
        ISubjectEntity subjectEntity = null;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Index == index)!;
        });
        if (subjectEntity is null) return null;

        var subject = _mapper.Map<Subject>(subjectEntity);

        return subject;
    }
    public async Task<bool> Update(string id, string title, string index, IReadOnlyCollection<string> teachersIds)
    {
        ISubjectEntity subjectEntity = null;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (subjectEntity is null) return false;

        subjectEntity.Title = title;
        subjectEntity.Index = index;
        subjectEntity.TeachersIds = teachersIds.ToList();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
