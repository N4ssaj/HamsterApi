
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface.Base;

namespace HamsterApi.Core.ServiceInterface;

public interface ISubjectService : IBaseService<Subject>
{
    public Task<bool> Update(string id, string title, string index, IReadOnlyCollection<string> teachersIds);
    public Task<Subject?> ReadByIndex(string index);
}
