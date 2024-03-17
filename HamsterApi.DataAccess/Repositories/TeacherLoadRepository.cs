

using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HamsterApi.DataAccess.Repositories;

public class TeacherLoadRepository : ITeacherLoadStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public TeacherLoadRepository(HamsterApiDbContext hamsterApiDbContext)
        => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<string> Create(TeacherLoad item)
    {
        var teacherLoadEntity = new TeacherLoadEntity()
        { Id = item.Id, 
            LaboratoryHours=item.LaboratoryHours, 
            LaboratoryHoursMax=item.LaboratoryHoursMax, 
            LecturesHours=item.LecturesHours, 
            LecturesHoursMax=item.LecturesHoursMax,
            PracticeHours=item.PracticeHours,
            PracticeHoursMax = item.PracticeHoursMax
        };
        await Task.Run(() =>
        {
            _hamsterApiDbContext.TeacherLoadEntities.Add(teacherLoadEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return teacherLoadEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        ITeacherLoadEntity teacherLoadEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            teacherLoadEntity = _hamsterApiDbContext.TeacherLoadEntities.FirstOrDefault(a => a.Id == id);
            if (teacherLoadEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(teacherLoadEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<TeacherLoad?> Read(string id)
    {
        ITeacherLoadEntity teacherLoadEntity = null;
        await Task.Run(() =>
        {
            teacherLoadEntity = _hamsterApiDbContext.TeacherLoadEntities.FirstOrDefault(a => a.Id == id);
        });
        if (teacherLoadEntity is null) return null;

        var teacherLoad = TeacherLoad.Create(teacherLoadEntity.Id, 
            teacherLoadEntity.LecturesHours, teacherLoadEntity.PracticeHours, teacherLoadEntity.LaboratoryHours,
            teacherLoadEntity.LecturesHoursMax, teacherLoadEntity.PracticeHoursMax, teacherLoadEntity.LaboratoryHoursMax);

        return teacherLoad.Value;
    }

    public async Task<List<TeacherLoad>?> ReadAll()
    {
        var teacherLoadEntityList = new List<ITeacherLoadEntity>();
        await Task.Run(() =>
        {
            teacherLoadEntityList = _hamsterApiDbContext.TeacherLoadEntities.ToList();
        }
        );
        var teacherLoadList = teacherLoadEntityList.Select(a => TeacherLoad.Create(a.Id, a.LecturesHours, a.PracticeHours, a.LaboratoryHours,
            a.LecturesHoursMax, a.PracticeHoursMax, a.LaboratoryHoursMax).Value).ToList();

        return teacherLoadList;
    }

    public async Task<bool> Update(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax)
    {
        ITeacherLoadEntity teacherLoadEntity = null;
        await Task.Run(() =>
        {
            teacherLoadEntity = _hamsterApiDbContext.TeacherLoadEntities.FirstOrDefault(a => a.Id == id);
        });
        if (teacherLoadEntity is null) return false;

        teacherLoadEntity.LecturesHours = lecturesHours;
        teacherLoadEntity.PracticeHours = practiceHours;
        teacherLoadEntity.LaboratoryHours = laboratoryHours;
        teacherLoadEntity.LecturesHoursMax = lecturesHoursMax;
        teacherLoadEntity.PracticeHoursMax = practiceHoursMax;
        teacherLoadEntity.LaboratoryHoursMax = laboratoryHoursMax;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
