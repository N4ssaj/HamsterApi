
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;


namespace HamsterApi.Core.Stores;

public interface IGroupStore:IBaseStore<Group>
{
    public Task<bool> Update(string id, string number, LevelOfEducation levelOfEducation,string directionId);
    public Task<Group?> ReadByNumber(string number);
}
