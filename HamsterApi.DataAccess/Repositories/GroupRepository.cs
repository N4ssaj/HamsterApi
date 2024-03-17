
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;

namespace HamsterApi.DataAccess.Repositories;

public class GroupRepository : IGroupStore
{
    public Task<string> Create(Group item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Group?> Read(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Group>?> ReadAll()
    {
        throw new NotImplementedException();
    }

    public Task<Group?> ReadByNumber(string number)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(string id, string number, LevelOfEducation levelOfEducation)
    {
        throw new NotImplementedException();
    }
}
