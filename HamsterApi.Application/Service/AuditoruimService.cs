
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class AuditoruimService : IAuditoriumService
{
    private readonly IAuditoriumStore _auditoriumStore;

    public AuditoruimService(IAuditoriumStore auditoriumStore)
        => _auditoriumStore = auditoriumStore;

    public async Task<string> Create(Auditorium item)
        =>await _auditoriumStore.Create(item);

    public async Task<bool> Delete(string id)
        => await _auditoriumStore.Delete(id);

    public async Task<Auditorium?> Read(string id)
        => await _auditoriumStore.Read(id);

    public async Task<List<Auditorium>?> ReadAll()
        => await _auditoriumStore.ReadAll();

    public async Task<List<Auditorium>?> ReadByIds(IEnumerable<string> ids)
        => await _auditoriumStore.ReadByIds(ids);

    public async Task<Auditorium?> ReadByNumber(string number)
        => await _auditoriumStore.ReadByNumber(number);

    public async Task<bool> Update(string id, string number)
        => await _auditoriumStore.Update(id, number);
}
