using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence.MappingExtensions;

namespace HamsterApi.Persistence.Repositories;

internal class ChairRepository : IChairRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public ChairRepository(HamsterApiDbContext hamsterApiDbContext)
            => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<bool> AddDepartment(string id, string departmentId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        return await Update(id,chair.Title,chair.TeachersIds,departmentId);
    }

    public async Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        foreach (var teacher in teacherId)
            if (!chair.TeachersIds.Contains(teacher))
                chair.AddTeacher(teacher);
        return await Update(chair.Id,chair.Title,chair.TeachersIds,chair.DepartmentId);
    }

    public async Task<bool> AddTeacherById(string id, string teacherId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        chair.AddTeacher(teacherId);

        return await Update(chair.Id, chair.Title, chair.TeachersIds, chair.DepartmentId);
    }

    public async Task<string> Create(Chair item)
    {
        var chairEntity = item.ToEntity();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.ChairEntities.Add(chairEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return chairEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        IChairEntity chairEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            chairEntity = _hamsterApiDbContext.ChairEntities.FirstOrDefault(a => a.Id == id)!;
            if (chairEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(chairEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Chair?> Read(string id)
    {
        IChairEntity chairEntity = null;
        await Task.Run(() =>
        {
            chairEntity = _hamsterApiDbContext.ChairEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (chairEntity is null) return null;

        var chair = chairEntity.ToModel();

        return chair;
    }

    public async Task<List<Chair>> ReadAll()
    {
        var chairEntityList = new List<IChairEntity>();
        await Task.Run(() =>
        {
            chairEntityList = _hamsterApiDbContext.ChairEntities.ToList();
        }
        );
        if (chairEntityList is null) return [];
        var chairList = chairEntityList.Select(a => a.ToModel()).ToList();

        return chairList;
    }

    public async Task<List<Chair>> ReadByIds(IEnumerable<string> ids)
    {
        var chairEntityList = new List<IChairEntity>();
        await Task.Run(() =>
        {
            chairEntityList = _hamsterApiDbContext.ChairEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (chairEntityList is null) return [];
        var chairList = chairEntityList.Select(a => a.ToModel()).ToList();

        return chairList;
    }

    public async Task<bool> RemoveDepartment(string id)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        return await Update(id, chair.Title, chair.TeachersIds,string.Empty);
    }

    public async Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        foreach (var teacher in teacherId)
            if (chair.TeachersIds.Contains(teacher))
                chair.RemoveTeacher(teacher);
        return await Update(chair.Id, chair.Title, chair.TeachersIds, chair.DepartmentId);
    }

    public async Task<bool> RemoveTeacherById(string id, string teacherId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        chair.RemoveTeacher(teacherId);
        return await Update(chair.Id, chair.Title, chair.TeachersIds, chair.DepartmentId);
    }

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> teachersIds, string departmentId)
    {
        IChairEntity chairEntity = null;
        await Task.Run(() =>
        {
            chairEntity = _hamsterApiDbContext.ChairEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (chairEntity is null) return false;
        chairEntity.Title = title;
        chairEntity.TeachersIds = teachersIds.ToList();
        chairEntity.DepartmentId = departmentId;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
