using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence;
using HamsterApi.Persistence.MappingExtensions;


internal class DepartmentRepository : IDepartmentRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;


    public DepartmentRepository(HamsterApiDbContext hamsterApiDbContext)
        => _hamsterApiDbContext = hamsterApiDbContext;

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
        var departmentEntity = item.ToEntity();
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
        var department = departmentEntity.ToModel();

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
        var departmentList = departmentEntityList.Select(a => a.ToModel()).ToList();

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
        var departmentList = departmentEntityList.Select(a => a.ToModel()).ToList();

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
