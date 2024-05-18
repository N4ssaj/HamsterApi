using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces.Base;

namespace HamsterApi.Domain.RepositoriesInterfaces;

public interface IAuditoriumRepository : IBaseRepository<Auditorium>
{
    public Task<bool> Update(string id, string number);
}
