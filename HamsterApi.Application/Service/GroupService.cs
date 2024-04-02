
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class GroupService : IGroupService
{
    private readonly IGroupStore _groupStore;

    public GroupService(IGroupStore groupStore)
        => _groupStore=groupStore;

    public async Task<string> Create(Group item)
        =>await _groupStore.Create(item);

    public async Task<bool> Delete(string id)
        => await _groupStore.Delete(id);

    public async Task<Group?> Read(string id)
        => await _groupStore.Read(id);

    public async Task<List<Group>> ReadAll()
        => await _groupStore.ReadAll();

    public async Task<List<Group>> ReadByIds(IEnumerable<string> ids)
        =>await _groupStore.ReadByIds(ids);
   

    public async Task<Group?> ReadByNumber(string number)
        =>await _groupStore.ReadByNumber(number);

    public async Task<bool> Update(string id, string number, LevelOfEducation levelOfEducation,string directionId)
        =>await _groupStore.Update(id,number, levelOfEducation,directionId);
}
