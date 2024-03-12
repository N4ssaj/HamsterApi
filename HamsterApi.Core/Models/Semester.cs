﻿
using CSharpFunctionalExtensions;

namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет семестр учебного года.
/// </summary>
public class Semester
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Semester"/> с указанным идентификатором, номером, группой и списком учебных дисциплин.
    /// </summary>
    /// <param name="id">Уникальный идентификатор семестра.</param>
    /// <param name="number">Номер семестра.</param>
    /// <param name="group">Группа, для которой проходит семестр.</param>
    /// <param name="subjects">Список учебных дисциплин в семестре вместе с их учебной нагрузкой.</param>
    private Semester(Guid id, int number, Group group, List<(Subject, AcademicLoad)> subjects)
        => (Id, Number, Group, Subjects) = (id, number, group, subjects);

    /// <summary>
    /// Получает уникальный идентификатор семестра.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Получает номер семестра.
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// Получает группу, для которой проходит семестр.
    /// </summary>
    public Group Group { get; }

    /// <summary>
    /// Получает список учебных дисциплин в семестре вместе с их учебной нагрузкой.
    /// </summary>
    public List<(Subject Subject, AcademicLoad AcademicLoad)> Subjects { get; }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="Semester"/> с указанным идентификатором, номером, группой и списком учебных дисциплин.
    /// </summary>
    /// <param name="id">Уникальный идентификатор семестра.</param>
    /// <param name="number">Номер семестра.</param>
    /// <param name="group">Группа, для которой проходит семестр.</param>
    /// <param name="subjects">Список учебных дисциплин в семестре вместе с их учебной нагрузкой.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Semester"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Semester> Create(Guid id, int number, Group group, List<(Subject, AcademicLoad)> subjects)
    {
        // Дополнительные валидации, если необходимо

        var semester = new Semester(id, number, group, subjects);
        return Result.Success(semester);
    }
}

