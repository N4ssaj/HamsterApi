using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;

namespace HamsterApi.Application.Service;

internal class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
        => _groupRepository=groupRepository;

    public async Task<bool> AddDirection(string id, string directionId)
        =>await _groupRepository.AddDirection(id, directionId);

    public async Task<string> Create(Group item)
        =>await _groupRepository.Create(item);

    public async Task<bool> Delete(string id)
        => await _groupRepository.Delete(id);

    public async Task<Group?> Read(string id)
        => await _groupRepository.Read(id);

    public async Task<List<Group>> ReadAll()
        => await _groupRepository.ReadAll();

    public async Task<List<Group>> ReadByIds(IEnumerable<string> ids)
        =>await _groupRepository.ReadByIds(ids);
   

    public async Task<Group?> ReadByNumber(string number)
        =>await _groupRepository.ReadByNumber(number);

    public async Task<bool> RemoveDirection(string id)
        =>await _groupRepository.RemoveDirection(id);

    public async Task<bool> Update(string id, string number, LevelOfEducation levelOfEducation,string directionId)
        =>await _groupRepository.Update(id,number, levelOfEducation,directionId);
}
