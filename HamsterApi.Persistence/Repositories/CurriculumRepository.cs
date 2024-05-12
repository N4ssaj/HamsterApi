

using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence.MappingExtensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HamsterApi.Persistence.Repositories;

internal class CurriculumRepository : ICurriculumRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public CurriculumRepository(HamsterApiDbContext hamsterApiDbContext)
            => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<bool> AddElectives(string id, IEnumerable<SubjectWtihLoad> subjectWtihLoads)
    {
        var item = await Read(id);
        if (item is null) return false;
        foreach (var subject in subjectWtihLoads)
            item.Add(subject);
        return await Update(id, item.ChairId, item.DepartmentId, item.DirectionId, item.SemestersSubjects.ToList(), item.SemestersElectiveSubjects.ToList(), item.YearOfPreparation, item.FGOSNumber); 
    }

    public async Task<bool> AddSubject(string id, IEnumerable<SubjectWtihLoad> subjectWtihLoads)
    {
        var item = await Read(id);
        if (item is null) return false;
        foreach (var subject in subjectWtihLoads)
            item.Add(subject);
        return await Update(id,item.ChairId,item.DepartmentId,item.DirectionId,item.SemestersSubjects.ToList(),item.SemestersElectiveSubjects.ToList(), item.YearOfPreparation,item.FGOSNumber);
    }

    public async Task<string> Create(Curriculum item)
    {
        var curriculumEntity = item.ToEntity();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.CurriculumEntities.Add(curriculumEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return curriculumEntity.Id; throw new NotImplementedException();
    }

    public async Task<bool> Delete(string id)
    {
        ICurriculumEntity curriculumEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            curriculumEntity = _hamsterApiDbContext.CurriculumEntities.FirstOrDefault(a => a.Id == id)!;
            if (curriculumEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(curriculumEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Curriculum?> Read(string id)
    {
        ICurriculumEntity curriculumEntity = null;
        await Task.Run(() =>
        {
            curriculumEntity = _hamsterApiDbContext.CurriculumEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (curriculumEntity is null) return null;

        var curriculum = curriculumEntity.ToModel();

        return curriculum;
    }

    public async Task<List<Curriculum>> ReadAll()
    {
        var curriculumEntityList = new List<ICurriculumEntity>();
        await Task.Run(() =>
        {
            curriculumEntityList = _hamsterApiDbContext.CurriculumEntities.ToList();
        }
        );
        if (curriculumEntityList is null) return [];
        var curriculumList = curriculumEntityList.Select(a => a.ToModel()).ToList();

        return curriculumList;
    }

    public async Task<List<Curriculum>> ReadByIds(IEnumerable<string> ids)
    {
        var curriculumEntityList = new List<ICurriculumEntity>();
        await Task.Run(() =>
        {
            curriculumEntityList = _hamsterApiDbContext.CurriculumEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (curriculumEntityList is null) return [];
        var curriculumList = curriculumEntityList.Select(a => a.ToModel()).ToList();

        return curriculumList;
    }

    public async Task<bool> RemoveElectives(string id, IEnumerable<string> subjectIds)
    {
        var item = await Read(id);
        if (item is null) return false;
        var list = item.SemestersElectiveSubjects.ToList();
        foreach (var idSubject in subjectIds)
            for (int i = 0; i < list.Count; i++)
                if (idSubject == list[i].Id)
                    list.Remove(list[i]);

        return await Update(id, item.ChairId, item.DepartmentId, item.DirectionId, item.SemestersSubjects.ToList(), list, item.YearOfPreparation, item.FGOSNumber); 
    }

    public async Task<bool> RemoveSubject(string id, IEnumerable<string> subjectIds)
    {
        var item = await Read(id);
        if (item is null) return false;
        var list = item.SemestersSubjects.ToList();
        foreach (var idSubject in subjectIds)
            for (int i = 0; i < list.Count; i++)
                if (idSubject == list[i].Id)
                    list.Remove(list[i]);

        return await Update(id, item.ChairId, item.DepartmentId, item.DirectionId, list, item.SemestersElectiveSubjects.ToList(), item.YearOfPreparation, item.FGOSNumber);
    }

    public async Task<bool> Update(string id, string chairId, string departmentId, string directionId, List<SubjectWtihLoad> curriculumsSubjects, List<SubjectWtihLoad> curriculumsElectiveSubjects, int yearOfPreparation, string fGOSNumber)
    {
        ICurriculumEntity curriculumEntity = null;
        await Task.Run(() =>
        {
            curriculumEntity = _hamsterApiDbContext.CurriculumEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (curriculumEntity is null) return false;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.DeleteObject(curriculumEntity);
            var item = Curriculum.Create(id,chairId,departmentId,directionId,curriculumsSubjects,curriculumsElectiveSubjects,yearOfPreparation,fGOSNumber).Value;
            _hamsterApiDbContext.CurriculumEntities.Add(item.ToEntity());
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
