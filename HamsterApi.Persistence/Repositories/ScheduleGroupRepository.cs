using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;



namespace HamsterApi.Persistence.Repositories;

internal class ScheduleGroupRepository:IScheduleGroupRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public ScheduleGroupRepository(HamsterApiDbContext hamsterApiDbContext)
        => (_hamsterApiDbContext) = (hamsterApiDbContext);

    public async Task<string> Create(ScheduleGroup item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ScheduleGroup?> Read(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ScheduleGroup>> ReadAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<ScheduleGroup>> ReadByIds(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(string id, string scheduleId, string groupId, int semesterNumber, List<ScheduledWeek> weeks)
    {
        throw new NotImplementedException();
    }
}
