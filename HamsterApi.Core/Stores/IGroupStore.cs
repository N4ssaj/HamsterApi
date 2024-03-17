
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface.Base;


namespace HamsterApi.Core.Stores;

public interface IGroupStore:IBaseService<Group>
{
    public Task<bool> Update(string id, string number, LevelOfEducation levelOfEducation);
    public Task<Group?> ReadByNumber(string number);
}
