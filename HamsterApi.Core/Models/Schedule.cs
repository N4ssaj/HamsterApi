﻿

using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет расписание занятий для определенного семестра.
/// </summary>
public class Schedule
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Schedule"/> с указанным идентификатором, номером семестра и расписанием групп.
    /// </summary>
    /// <param name="id">Уникальный идентификатор расписания.</param>
    /// <param name="semesterNumber">Номер семестра, для которого составлено расписание.</param>
    /// <param name="groupsSchedule">Расписание групп, где ключ - группа, а значение - расписание для этой группы.</param>
    private Schedule(string id , int semesterNumber, IReadOnlyCollection<ScheduleGroup> groupsSchedule)
        => (Id, SemesterNumber, GroupsSchedule) = (id, semesterNumber, groupsSchedule);

    /// <summary>
    /// Получает уникальный идентификатор расписания.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Получает номер семестра, для которого составлено расписание.
    /// </summary>
    public int SemesterNumber { get; }

    /// <summary>
    /// Получает расписание групп, где ключ - группа, а значение - расписание для этой группы.
    /// </summary>
    public IReadOnlyCollection<ScheduleGroup> GroupsSchedule { get; }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="Schedule"/> с указанным идентификатором, номером семестра и расписанием групп.
    /// </summary>
    /// <param name="id">Уникальный идентификатор расписания.</param>
    /// <param name="semesterNumber">Номер семестра, для которого составлено расписание.</param>
    /// <param name="groupsSchedule">Расписание групп, где ключ - группа, а значение - расписание для этой группы.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Schedule"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Schedule> Create(string id , int semesterNumber, IReadOnlyCollection<ScheduleGroup> groupsSchedule)
    {
        // Дополнительные валидации, если необходимо

        var schedule = new Schedule(id, semesterNumber, groupsSchedule);
        return schedule;
    }
}

