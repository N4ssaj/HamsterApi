
using CSharpFunctionalExtensions;

namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет расписание занятий для определенной группы.
/// </summary>
public class ScheduleGroup
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ScheduleGroup"/> с указанным идентификатором, группой, номером семестра и недельным расписанием занятий.
    /// </summary>
    /// <param name="id">Уникальный идентификатор расписания группы.</param>
    /// <param name="group">Группа, для которой составлено расписание.</param>
    /// <param name="semesterNumber">Номер семестра, для которого составлено расписание.</param>
    /// <param name="weeks">Недельное расписание занятий, где ключ - номер недели, а значение - расписание по дням недели.</param>
    private ScheduleGroup(Guid id, Group group, uint semesterNumber, Dictionary<int, Dictionary<DayOfWeek, List<ScheduledClass>>> weeks)
        => (Id, Group, SemesterNumber, Weeks) = (id, group, semesterNumber, weeks);

    /// <summary>
    /// Получает уникальный идентификатор расписания группы.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Получает группу, для которой составлено расписание.
    /// </summary>
    public Group Group { get; }

    /// <summary>
    /// Получает номер семестра, для которого составлено расписание.
    /// </summary>
    public uint SemesterNumber { get; }

    /// <summary>
    /// Получает недельное расписание занятий, где ключ - номер недели, а значение - расписание по дням недели.
    /// </summary>
    public Dictionary<int, Dictionary<DayOfWeek, List<ScheduledClass>>> Weeks { get; }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="ScheduleGroup"/> с указанным идентификатором, группой, номером семестра и недельным расписанием занятий.
    /// </summary>
    /// <param name="id">Уникальный идентификатор расписания группы.</param>
    /// <param name="group">Группа, для которой составлено расписание.</param>
    /// <param name="semesterNumber">Номер семестра, для которого составлено расписание.</param>
    /// <param name="weeks">Недельное расписание занятий, где ключ - номер недели, а значение - расписание по дням недели.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="ScheduleGroup"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<ScheduleGroup> Create(Guid id, Group group, uint semesterNumber, Dictionary<int, Dictionary<DayOfWeek, List<ScheduledClass>>> weeks)
    {
        // Дополнительные валидации, если необходимо

        var scheduleGroup = new ScheduleGroup(id, group, semesterNumber, weeks);
        return Result.Success(scheduleGroup);
    }
}

