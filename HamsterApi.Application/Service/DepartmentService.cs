using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;

namespace HamsterApi.Application.Service;

internal class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
        => _departmentRepository = departmentRepository;

    public async Task<bool> AddRangeChairById(string id, IEnumerable<string> chairId)
        =>await _departmentRepository.AddRangeChairById(id, chairId);

    public async Task<bool> AddRangeDirectionById(string id, IEnumerable<string> directionId)
        =>await _departmentRepository.AddRangeDirectionById(id, directionId);

    public async Task<string> Create(Department item)
        =>await _departmentRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await _departmentRepository.Delete(id);

    public async Task<Department?> Read(string id)
        =>await _departmentRepository.Read(id);

    public async Task<List<Department>> ReadAll()
        =>await _departmentRepository.ReadAll();

    public async Task<List<Department>> ReadByIds(IEnumerable<string> ids)
        =>await _departmentRepository.ReadByIds(ids);

    public async Task<bool> RemoveRangeChairById(string id, IEnumerable<string> chairId)
        =>await _departmentRepository.RemoveRangeChairById(id, chairId);

    public async Task<bool> RemoveRangeDirectionById(string id, IEnumerable<string> directionId)
        =>await _departmentRepository.RemoveRangeDirectionById(id, directionId);

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> chairsIds, IReadOnlyCollection<string> directionsIds)
        =>await _departmentRepository.Update(id,title, chairsIds, directionsIds);
}
