using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет запланированное занятие.
/// </summary>
public class ScheduledClass
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ScheduledClass"/> с указанным идентификатором, номером занятия, предметом, преподавателем и аудиторией.
    /// </summary>
    /// <param name="id">Уникальный идентификатор занятия.</param>
    /// <param name="classNumber">Номер занятия в расписании.</param>
    /// <param name="subject">Предмет занятия.</param>
    /// <param name="teacher">Преподаватель, ведущий занятие.</param>
    /// <param name="auditorium">Аудитория, в которой проводится занятие.</param>
    private ScheduledClass(Guid id, uint classNumber, Subject subject, Teacher teacher, Auditorium auditorium)
        => (Id, ClassNumber, Subject, Teacher, Auditorium) = (id, classNumber, subject, teacher, auditorium);

    /// <summary>
    /// Получает уникальный идентификатор занятия.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Получает номер занятия в расписании.
    /// </summary>
    public uint ClassNumber { get; }

    /// <summary>
    /// Получает предмет занятия.
    /// </summary>
    public Subject Subject { get; }

    /// <summary>
    /// Получает преподавателя, ведущего занятие.
    /// </summary>
    public Teacher Teacher { get; }

    /// <summary>
    /// Получает аудиторию, в которой проводится занятие.
    /// </summary>
    public Auditorium Auditorium { get; }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="ScheduledClass"/> с указанным идентификатором, номером занятия, предметом, преподавателем и аудиторией.
    /// </summary>
    /// <param name="id">Уникальный идентификатор занятия.</param>
    /// <param name="classNumber">Номер занятия в расписании.</param>
    /// <param name="subject">Предмет занятия.</param>
    /// <param name="teacher">Преподаватель, ведущий занятие.</param>
    /// <param name="auditorium">Аудитория, в которой проводится занятие.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="ScheduledClass"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<ScheduledClass> Create(Guid id, uint classNumber, Subject subject, Teacher teacher, Auditorium auditorium)
    {
        // Возможные дополнительные проверки

        var scheduledClass = new ScheduledClass(id, classNumber, subject, teacher, auditorium);
        return Result.Success(scheduledClass);
    }
}

