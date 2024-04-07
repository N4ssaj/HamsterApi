
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class DepartmentService : IDepartmentService
{

    private readonly IDepartmentStore _departmentStore;

    public DepartmentService(IDepartmentStore departmentStore)
        => _departmentStore = departmentStore;

    public async Task<bool> AddRangeChairById(string id, IEnumerable<string> chairId)
        =>await _departmentStore.AddRangeChairById(id, chairId);

    public async Task<bool> AddRangeDirectionById(string id, IEnumerable<string> directionId)
        =>await _departmentStore.AddRangeDirectionById(id, directionId);

    public async Task<string> Create(Department item)
        =>await _departmentStore.Create(item);

    public async Task<bool> Delete(string id)
        =>await _departmentStore.Delete(id);

    public async Task<Department?> Read(string id)
        =>await _departmentStore.Read(id);

    public async Task<List<Department>> ReadAll()
        =>await _departmentStore.ReadAll();

    public async Task<List<Department>> ReadByIds(IEnumerable<string> ids)
        =>await _departmentStore.ReadByIds(ids);

    public async Task<bool> RemoveRangeChairById(string id, IEnumerable<string> chairId)
        =>await _departmentStore.RemoveRangeChairById(id, chairId);

    public async Task<bool> RemoveRangeDirectionById(string id, IEnumerable<string> directionId)
        =>await _departmentStore.RemoveRangeDirectionById(id, directionId);

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> chairsIds, IReadOnlyCollection<string> directionsIds)
        =>await _departmentStore.Update(id,title, chairsIds, directionsIds);
}
