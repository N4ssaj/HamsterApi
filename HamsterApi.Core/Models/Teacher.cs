
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

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
    public Teacher(Guid id, string name, string surname, string patronymic, string fullName, IReadOnlyCollection<Subject> subjects)
        => (Id, Name, Surname, Patronymic, FullName, Subjects) = (id, name, surname, patronymic, fullName, subjects);

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
    public Guid Id { get; }

    /// <summary>
    /// Создает нового учителя с указанным идентификатором, именем, фамилией, отчеством и списком преподаваемых дисциплин.
    /// </summary>
    /// <param name="id">Уникальный идентификатор учителя.</param>
    /// <param name="name">Имя учителя.</param>
    /// <param name="surname">Фамилия учителя.</param>
    /// <param name="patronymic">Отчество учителя.</param>
    /// <param name="subjects">Список преподаваемых дисциплин.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Teacher"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Teacher> Create(Guid id, string name, string surname, string patronymic, IReadOnlyCollection<Subject> subjects)
    {
        string fullName = string.Join(" ", name, surname, patronymic);

        var teacher = new Teacher(id, name, surname, patronymic, fullName, subjects);

        return Result.Success(teacher);
    }
}
