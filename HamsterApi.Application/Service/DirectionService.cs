
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class DirectionService : IDirectionService
{
    private readonly IDirectionStore _directionStore;

    public DirectionService(IDirectionStore directionStore)
        =>_directionStore = directionStore;
 
    public async Task<bool> AddGroupById(string id, string groupId)
        =>await _directionStore.AddGroupById(id, groupId);

    public async Task<bool> AddRangeGroupById(string id, IEnumerable<string> groupId)
        =>await _directionStore.AddRangeGroupById(id,groupId);

    public async Task<string> Create(Direction item)
        =>await _directionStore.Create(item);

    public async Task<bool> Delete(string id)
        =>await _directionStore.Delete(id);

    public async Task<Direction?> Read(string id)
        =>await _directionStore.Read(id);

    public async Task<List<Direction>> ReadAll()
        =>await _directionStore.ReadAll();

    public async Task<List<Direction>> ReadByIds(IEnumerable<string> ids)
        =>await _directionStore.ReadByIds(ids);

    public async Task<bool> RemoveGroupById(string id, string groupId)
        =>await _directionStore.RemoveGroupById(id, groupId);

    public async Task<bool> RemoveRangeGroupById(string id, IEnumerable<string> groupId)
        =>await _directionStore.RemoveRangeGroupById(id, groupId);  

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> groupsIds, FormOfEducation formOfEducation, LevelOfEducation levelOfEducation, string departmentId)
        =>await _directionStore.Update(id,title, groupsIds, formOfEducation, levelOfEducation, departmentId);
}
