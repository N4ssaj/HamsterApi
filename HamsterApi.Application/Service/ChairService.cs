using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;

namespace HamsterApi.Application.Service;

internal class ChairService : IChairService
{
    private readonly IChairRepository _chairRepository;

    public ChairService(IChairRepository chairRepository)
        => _chairRepository = chairRepository;

    public async Task<bool> AddDepartment(string id, string departmentId)
        =>await _chairRepository.AddDepartment(id, departmentId);

    public async Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId)
        =>await _chairRepository.AddRangeTeacherById(id, teacherId);

    public async Task<bool> AddTeacherById(string id, string teacherId)
        =>await AddTeacherById(id, teacherId);

    public async Task<string> Create(Chair item)
        =>await _chairRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await _chairRepository.Delete(id);

    public async Task<Chair?> Read(string id)
        =>await _chairRepository.Read(id);

    public async Task<List<Chair>> ReadAll()
        =>await _chairRepository.ReadAll();

    public async Task<List<Chair>> ReadByIds(IEnumerable<string> ids)
        =>await _chairRepository.ReadByIds(ids);

    public async Task<bool> RemoveDepartment(string id)
        =>await _chairRepository.RemoveDepartment(id);

    public async Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId)
        =>await _chairRepository.RemoveRangeTeacherById(id,teacherId);

    public async Task<bool> RemoveTeacherById(string id, string teacherId)
        => await _chairRepository.RemoveTeacherById(id, teacherId);

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> teachersIds, string departmentId)
        =>await _chairRepository.Update(id,title, teachersIds, departmentId);
}
