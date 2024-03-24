
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class TeachingLoadService : ITeacherLoadService
{
    private readonly ITeachingLoadStore _teacherLoadStore;

    public TeachingLoadService(ITeachingLoadStore teacherLoadStore)
        =>_teacherLoadStore=teacherLoadStore;

    public async Task<string> Create(TeachingLoad item)
        =>await _teacherLoadStore.Create(item);

    public async Task<bool> Delete(string id)
        =>await _teacherLoadStore.Delete(id);

    public async Task<TeachingLoad?> Read(string id)
        =>await _teacherLoadStore.Read(id);

    public async Task<List<TeachingLoad>?> ReadAll()
        =>await _teacherLoadStore.ReadAll();

    public async Task<List<TeachingLoad>?> ReadByIds(IEnumerable<string> ids)
        =>await _teacherLoadStore.ReadByIds(ids);

    public async Task<bool> Update(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax)
        => await _teacherLoadStore.Update(id, lecturesHours, practiceHours, laboratoryHours, lecturesHoursMax, practiceHoursMax, laboratoryHoursMax);
}
