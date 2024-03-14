using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;

namespace HamsterApi.Core.Stores;

public interface IAuditoriumStore:IBaseStore<Auditorium>
{
    public Task<bool> Update(string id, string number);

    public Task<Auditorium?> ReadByNumber(string number);
}
