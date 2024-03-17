
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class TeacherLoadService : ITeacherLoadService
{
    private readonly ITeacherLoadStore _teacherLoadStore;

    public TeacherLoadService(ITeacherLoadStore teacherLoadStore)
        =>_teacherLoadStore=teacherLoadStore;

    public async Task<string> Create(TeacherLoad item)
        =>await _teacherLoadStore.Create(item);

    public async Task<bool> Delete(string id)
        =>await _teacherLoadStore.Delete(id);

    public async Task<TeacherLoad?> Read(string id)
        =>await _teacherLoadStore.Read(id);

    public async Task<List<TeacherLoad>?> ReadAll()
        =>await _teacherLoadStore.ReadAll();

    public async Task<bool> Update(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax)
        => await _teacherLoadStore.Update(id, lecturesHours, practiceHours, laboratoryHours, lecturesHoursMax, practiceHoursMax, laboratoryHoursMax);
}
