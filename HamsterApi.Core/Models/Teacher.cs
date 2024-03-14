

using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет учителя.
/// </summary>
public class Teacher
{
    /// <summary>
    /// Инициализирует нового учителя с указанным идентификатором, именем, фамилией, отчеством, полным именем и списком преподаваемых дисциплин.
    /// </summary>
    /// <param name="id">Уникальный идентификатор учителя.</param>
    /// <param name="name">Имя учителя.</param>
    /// <param name="surname">Фамилия учителя.</param>
    /// <param name="patronymic">Отчество учителя.</param>
    /// <param name="fullName">Полное имя учителя.</param>
    /// <param name="subjects">Список преподаваемых дисциплин.</param>
    public Teacher(string id , string name, string surname, string patronymic, string fullName, IReadOnlyCollection<Subject> subjects, Department department, TeacherLoad teacherLoad)
        => (Id, Name, Surname, Patronymic, FullName, Subjects,Department,TeacherLoad) 
        = (id, name, surname, patronymic, fullName, subjects,department, teacherLoad);

    /// <summary>
    /// Получает имя учителя.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Получает фамилию учителя.
    /// </summary>
    public string Surname { get; }

    /// <summary>
    /// Получает отчество учителя.
    /// </summary>
    public string Patronymic { get; }

    /// <summary>
    /// Получает полное имя учителя.
    /// </summary>
    public string FullName { get; }

    /// <summary>
    /// Получает список преподаваемых дисциплин.
    /// </summary>
    public IReadOnlyCollection<Subject> Subjects { get; }

    /// <summary>
    /// Получает уникальный идентификатор учителя.
    /// </summary>
    public string Id { get; }

    public Department Department { get; }

    public TeacherLoad TeacherLoad { get; }

    /// <summary>
    /// Создает нового учителя с указанным идентификатором, именем, фамилией, отчеством и списком преподаваемых дисциплин.
    /// </summary>
    /// <param name="id">Уникальный идентификатор учителя.</param>
    /// <param name="name">Имя учителя.</param>
    /// <param name="surname">Фамилия учителя.</param>
    /// <param name="patronymic">Отчество учителя.</param>
    /// <param name="subjects">Список преподаваемых дисциплин.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Teacher"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Teacher> Create(string id , string name, string surname, string patronymic, IReadOnlyCollection<Subject> subjects, Department department, TeacherLoad teacherLoad)
    {
        string fullName = string.Join(" ", surname,name, patronymic);

        var teacher = new Teacher(id, name, surname, patronymic, fullName, subjects,department,teacherLoad);

        return teacher;
    }
}
