

using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;


public class Teacher
{
    private Teacher(string id, string name, string surname, string patronymic, string fullName, List<string> subjectsIds, string chairId, string teacherLoadId)
        => (Id, Name, Surname, Patronymic, FullName, _subjectsIds, ChairId, TeacherLoadId)
        = (id, name, surname, patronymic, fullName, subjectsIds, chairId, teacherLoadId);

    public string Name { get; } = string.Empty;

    public string Surname { get; } = string.Empty;

    public string Patronymic { get; } = string.Empty;

    public string FullName { get; } = string.Empty;

    private List<string> _subjectsIds = [];

    public IReadOnlyCollection<string> SubjectsIds
        => _subjectsIds;

    public void AddSubject(string subject)
        => _subjectsIds.Add(subject);


    public void RemoveSubject(string subject)
        => _subjectsIds.Remove(subject);

    public string Id { get; } = string.Empty;

    public string ChairId { get; } = string.Empty;

    public string TeacherLoadId { get; } = string.Empty;

    public static Result<Teacher> Create(string id, string name, string surname, string patronymic, List<string> subjectsIds, string chairId, string teacherLoadId)
    {
        string fullName = string.Join(" ", surname, name, patronymic);

        var teacher = new Teacher(id, name, surname, patronymic, fullName, subjectsIds, chairId, teacherLoadId);

        return teacher;
    }
}
