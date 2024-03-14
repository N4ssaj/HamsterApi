

using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

public class TeacherLoad
{
    private TeacherLoad(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax)
        => (Id, LecturesHours, PracticeHours, LaboratoryHours, LecturesHoursMax, PracticeHoursMax, LaboratoryHoursMax)
        = (id, lecturesHours, practiceHours, laboratoryHours, lecturesHoursMax, practiceHoursMax, laboratoryHoursMax);
   
    public string Id { get; }

    public int LecturesHours { get; }

    public int PracticeHours { get; }

    public int LaboratoryHours { get; }

    public int LecturesHoursMax { get; }

    public int PracticeHoursMax { get; }

    public int LaboratoryHoursMax { get; }

    public static Result<TeacherLoad> Create(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax)
    {
        var teacherLoad = new TeacherLoad(id,lecturesHours,practiceHours,laboratoryHours,lecturesHoursMax,practiceHoursMax,laboratoryHoursMax);
        return teacherLoad;
    }
}
