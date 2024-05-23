using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HamsterApi.Application.Service;

internal class ScheduleGroupService : IScheduleGroupService
{
    private readonly IScheduleGroupRepository _scheduleGroupRepository;

    public ScheduleGroupService(
        IScheduleGroupRepository scheduleGroupRepository)
    {
        _scheduleGroupRepository = scheduleGroupRepository;
    }

    public async Task<string> Create(ScheduleGroup item)
    {
        return await _scheduleGroupRepository.Create(item);
    }

    public async Task<bool> Delete(string id)
    {
        return await _scheduleGroupRepository.Delete(id);
    }

    public async Task<ScheduleGroup?> Read(string id)
    {
        return await _scheduleGroupRepository.Read(id);
    }

    public async Task<List<ScheduleGroup>> ReadAll()
    {
        return await _scheduleGroupRepository.ReadAll();
    }

    public async Task<List<ScheduleGroup>> ReadByIds(IEnumerable<string> ids)
    {
        return await _scheduleGroupRepository.ReadByIds(ids);
    }

    public async Task<bool> Update(string id, string scheduleId, string groupId, int semesterNumber, List<ScheduledWeek> weeks)
    {
        return await _scheduleGroupRepository.Update(id,scheduleId, groupId, semesterNumber, weeks);
    }
}

