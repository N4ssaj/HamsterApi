
using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface.Base;


namespace HamsterApi.Domain.ServiceInterface;

public interface IGroupService : IBaseService<Group>
{
    public Task<bool> Update(string id, string number, LevelOfEducation levelOfEducation, string directionId);
    public Task<Group?> ReadByNumber(string number);
    public Task<bool> AddDirection(string id, string directionId);
    public Task<bool> RemoveDirection(string id);
}
