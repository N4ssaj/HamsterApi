using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;

namespace HamsterApi.Application.Service;

internal class ScheduleService:IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleService(IScheduleRepository scheduleRepository)
        => _scheduleRepository = scheduleRepository;

    public async Task<bool> AddGroup(string id, IEnumerable<string> groupIds)
        =>await _scheduleRepository.AddGroup(id, groupIds);

    public async Task<string> Create(Schedule item)
        =>await _scheduleRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await _scheduleRepository.Delete(id);

    public async Task<Schedule?> Read(string id)
        =>await _scheduleRepository.Read(id);

    public async Task<List<Schedule>> ReadAll()
        =>await _scheduleRepository.ReadAll();

    public async Task<List<Schedule>> ReadByIds(IEnumerable<string> ids)
        =>await _scheduleRepository.ReadByIds(ids);

    public async Task<bool> RemoveGroup(string id, IEnumerable<string> groupIds)
        =>await _scheduleRepository.RemoveGroup(id,groupIds);

    public async Task<bool> Update(string id, int semesterNumber, IReadOnlyCollection<string> groupsScheduleIds)
        =>await _scheduleRepository.Update(id, semesterNumber, groupsScheduleIds);
}
