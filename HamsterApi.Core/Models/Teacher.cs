

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
    /// <param name="subjectsIds">Список преподаваемых дисциплин.</param>
    public Teacher(string id , string name, string surname, string patronymic, string fullName, IReadOnlyCollection<string> subjectsIds, string chairId, string teacherLoadId)
        => (Id, Name, Surname, Patronymic, FullName, SubjectsIds,ChairId,TeacherLoadId) 
        = (id, name, surname, patronymic, fullName, subjectsIds,chairId, teacherLoadId);

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
    public IReadOnlyCollection<string> SubjectsIds { get; }

    /// <summary>
    /// Получает уникальный идентификатор учителя.
    /// </summary>
    public string Id { get; }

    public string ChairId { get; }

    public string TeacherLoadId { get; }

    /// <summary>
    /// Создает нового учителя с указанным идентификатором, именем, фамилией, отчеством и списком преподаваемых дисциплин.
    /// </summary>
    /// <param name="id">Уникальный идентификатор учителя.</param>
    /// <param name="name">Имя учителя.</param>
    /// <param name="surname">Фамилия учителя.</param>
    /// <param name="patronymic">Отчество учителя.</param>
    /// <param name="subjectsIds">Список преподаваемых дисциплин.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Teacher"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Teacher> Create(string id , string name, string surname, string patronymic, IReadOnlyCollection<string> subjectsIds, string chairId, string teacherLoadId)
    {
        string fullName = string.Join(" ", surname,name, patronymic);

        var teacher = new Teacher(id, name, surname, patronymic, fullName, subjectsIds,chairId,teacherLoadId);

        return teacher;
    }
}
