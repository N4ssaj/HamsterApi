
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;

namespace HamsterApi.Core.Stores;

public interface ISubjectStore:IBaseStore<Subject>
{
    public Task<bool> Update(string id, string title, string index, IReadOnlyCollection<Teacher> teachers);
    public Task<Group?> ReadByIndex(string index);
}
