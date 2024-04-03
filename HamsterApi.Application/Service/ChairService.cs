
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class ChairService : IChairService
{
    private readonly IChairStore _chairStore;

    public ChairService(IChairStore chairStore)
        => _chairStore = chairStore;

    public async Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId)
        =>await _chairStore.AddRangeTeacherById(id, teacherId);

    public async Task<bool> AddTeacherById(string id, string teacherId)
        =>await AddTeacherById(id, teacherId);

    public async Task<string> Create(Chair item)
        =>await _chairStore.Create(item);

    public async Task<bool> Delete(string id)
        =>await _chairStore.Delete(id);

    public async Task<Chair?> Read(string id)
        =>await _chairStore.Read(id);

    public async Task<List<Chair>> ReadAll()
        =>await _chairStore.ReadAll();

    public async Task<List<Chair>> ReadByIds(IEnumerable<string> ids)
        =>await _chairStore.ReadByIds(ids);

    public async Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId)
        =>await _chairStore.RemoveRangeTeacherById(id,teacherId);

    public async Task<bool> RemoveTeacherById(string id, string teacherId)
        => await _chairStore.RemoveTeacherById(id, teacherId);

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> teachersIds, string departmentId)
        =>await _chairStore.Update(id,title, teachersIds, departmentId);
}
