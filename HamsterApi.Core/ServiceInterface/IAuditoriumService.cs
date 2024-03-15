
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface.Base;

namespace HamsterApi.Core.ServiceInterface;

public interface IAuditoriumService:IBaseService<Auditorium>
{
    public Task<bool> Update(string id, string number);

    public Task<Auditorium?> ReadByNumber(string number);
}
