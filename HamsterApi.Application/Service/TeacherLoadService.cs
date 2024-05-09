using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;

namespace HamsterApi.Application.Service;

public class TeacherLoadService : ITeacherLoadService
{
    private readonly ITeachingLoadRepository _teacherLoadRepository;

    public TeacherLoadService(ITeachingLoadRepository teacherLoadRepository)
        =>_teacherLoadRepository=teacherLoadRepository;

    public async Task<string> Create(TeachingLoad item)
        =>await _teacherLoadRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await _teacherLoadRepository.Delete(id);

    public async Task<TeachingLoad?> Read(string id)
        =>await _teacherLoadRepository.Read(id);

    public async Task<List<TeachingLoad>> ReadAll()
        =>await _teacherLoadRepository.ReadAll();

    public async Task<List<TeachingLoad>> ReadByIds(IEnumerable<string> ids)
        =>await _teacherLoadRepository.ReadByIds(ids);

    public async Task<bool> Update(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax)
        => await _teacherLoadRepository.Update(id, lecturesHours, practiceHours, laboratoryHours, lecturesHoursMax, practiceHoursMax, laboratoryHoursMax);
}
