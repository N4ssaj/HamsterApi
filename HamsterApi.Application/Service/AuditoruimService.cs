using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;

namespace HamsterApi.Application.Service;

internal class AuditoruimService : IAuditoriumService
{
    private readonly IAuditoriumRepository _auditoriumRepository;

    public AuditoruimService(IAuditoriumRepository auditoriumRepository)
        => _auditoriumRepository = auditoriumRepository;

    public async Task<string> Create(Auditorium item)
        =>await _auditoriumRepository.Create(item);

    public async Task<bool> Delete(string id)
        => await _auditoriumRepository.Delete(id);

    public async Task<Auditorium?> Read(string id)
        => await _auditoriumRepository.Read(id);

    public async Task<List<Auditorium>> ReadAll()
        => await _auditoriumRepository.ReadAll();

    public async Task<List<Auditorium>> ReadByIds(IEnumerable<string> ids)
        => await _auditoriumRepository.ReadByIds(ids);

    public async Task<Auditorium?> ReadByNumber(string number)
        => await _auditoriumRepository.ReadByNumber(number);

    public async Task<bool> Update(string id, string number)
        => await _auditoriumRepository.Update(id, number);
}
