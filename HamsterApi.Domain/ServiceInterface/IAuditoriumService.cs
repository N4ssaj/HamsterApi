
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface.Base;

namespace HamsterApi.Domain.ServiceInterface;

public interface IAuditoriumService : IBaseService<Auditorium>
{
    public Task<bool> Update(string id, string number);

    public Task<Auditorium?> ReadByNumber(string number);
}
