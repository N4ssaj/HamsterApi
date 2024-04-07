

using AutoMapper;
using HamsterApi.Core.Models;
using HamsterApi.DataAccess;
using HamsterApi.DataAccess.Entites.Interfaces;
using System.Xml.Linq;

public class DepartmentRepository : IDepartmentStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    private readonly IMapper _mapper;

    public DepartmentRepository(HamsterApiDbContext hamsterApiDbContext, IMapper mapper)
        => (_hamsterApiDbContext, _mapper) = (hamsterApiDbContext, mapper);

    public async Task<bool> AddRangeChairById(string id, IEnumerable<string> chairId)
    {
        var department = await Read(id);
        if (department is null) return false;
        foreach (var chair in chairId)
            if (!department.ChairsIds.Contains(chair))
                department.AddChair(chair);
        return await Update(id, department.Title,department.ChairsIds,department.DirectionsIds);
    }

    public async Task<bool> AddRangeDirectionById(string id, IEnumerable<string> directionId)
    {
        var department = await Read(id);
        if (department is null) return false;
        foreach (var direction in directionId)
            if (!department.DirectionsIds.Contains(direction))
                department.AddDirection(direction);
        return await Update(id, department.Title, department.ChairsIds, department.DirectionsIds);
    }

    public async Task<string> Create(Department item)
    {
        var departmentEntity = _mapper.Map<DepartmentEntity>(item);
        await Task.Run(() =>
        {
            _hamsterApiDbContext.DepartmentEntities.Add(departmentEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return departmentEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        IDepartmentEntity departmentEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            departmentEntity = _hamsterApiDbContext.DepartmentEntities.FirstOrDefault(a => a.Id == id)!;
            if (departmentEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(departmentEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Department?> Read(string id)
    {
        IDepartmentEntity departmentEntity = null!;
        await Task.Run(() =>
        {
            departmentEntity = _hamsterApiDbContext.DepartmentEntities.FirstOrDefault(a=>a.Id==id);
        }
        );
        if (departmentEntity is null) return null;
        var department = _mapper.Map<Department>(departmentEntity);

        return department;
    }

    public async Task<List<Department>> ReadAll()
    {
        var departmentEntityList = new List<IDepartmentEntity>();
        await Task.Run(() =>
        {
            departmentEntityList = _hamsterApiDbContext.DepartmentEntities.ToList();
        }
        );
        if (departmentEntityList is null) return [];
        var departmentList = departmentEntityList.Select(a => _mapper.Map<Department>(a)).ToList();

        return departmentList;
    }

    public async Task<List<Department>> ReadByIds(IEnumerable<string> ids)
    {
        var departmentEntityList = new List<IDepartmentEntity>();
        await Task.Run(() =>
        {
            departmentEntityList = _hamsterApiDbContext.DepartmentEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (departmentEntityList is null) return [];
        var departmentList = departmentEntityList.Select(a => _mapper.Map<Department>(a)).ToList();

        return departmentList;
    }

    public async Task<bool> RemoveRangeChairById(string id, IEnumerable<string> chairId)
    {

        var department = await Read(id);
        if (department is null) return false;
        foreach (var chair in chairId)
            if (department.ChairsIds.Contains(chair))
                department.RemoveChair(chair);
        return await Update(id, department.Title, department.ChairsIds, department.DirectionsIds);
    }

    public async Task<bool> RemoveRangeDirectionById(string id, IEnumerable<string> directionId)
    {

        var department = await Read(id);
        if (department is null) return false;
        foreach (var direction in directionId)
            if (department.DirectionsIds.Contains(direction))
                department.RemoveDirection(direction);
        return await Update(id, department.Title, department.ChairsIds, department.DirectionsIds);
    }

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> chairsIds, IReadOnlyCollection<string> directionsIds)
    {
        IDepartmentEntity departmentEntity = null;
        await Task.Run(() =>
        {
            departmentEntity = _hamsterApiDbContext.DepartmentEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (departmentEntity is null) return false;
        departmentEntity.Title = title;
        departmentEntity.ChairsIds = chairsIds.ToList();
        departmentEntity.DirectionsIds = directionsIds.ToList();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
