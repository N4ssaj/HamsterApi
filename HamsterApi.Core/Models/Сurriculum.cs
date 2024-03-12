
using CSharpFunctionalExtensions;

namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет учебный план для определенной группы.
/// </summary>
public class Curriculum
{
    /// <summary>
    /// Инициализирует новый учебный план с указанным идентификатором, группой, списком дисциплин по семестрам и списком выборочных дисциплин по семестрам.
    /// </summary>
    /// <param name="id">Уникальный идентификатор учебного плана.</param>
    /// <param name="group">Группа, для которой составлен учебный план.</param>
    /// <param name="semestersSubjects">Словарь, содержащий списки дисциплин по семестрам.</param>
    /// <param name="semestersElectiveSubjects">Словарь, содержащий списки выборочных дисциплин по семестрам.</param>
    private Curriculum(Guid id, Group group, Dictionary<uint, List<(Subject, AcademicLoad)>> semestersSubjects, Dictionary<uint, List<(Subject, AcademicLoad)>> semestersElectiveSubjects)
        => (Id, Group, SemestersSubjects, SemestersSubjects) = (id, group, semestersSubjects, semestersElectiveSubjects);

    /// <summary>
    /// Получает уникальный идентификатор учебного плана.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Получает группу, для которой составлен учебный план.
    /// </summary>
    public Group Group { get; }

    /// <summary>
    /// Получает словарь, содержащий списки дисциплин по семестрам.
    /// </summary>
    public Dictionary<uint, List<(Subject Subject, AcademicLoad AcademicLoad)>> SemestersSubjects { get; }

    /// <summary>
    /// Получает словарь, содержащий списки выборочных дисциплин по семестрам.
    /// </summary>
    public Dictionary<uint, List<(Subject Subject, AcademicLoad AcademicLoad)>> SemestersElectiveSubjects { get; }

    /// <summary>
    /// Создает новый учебный план с указанным идентификатором, группой, списком дисциплин по семестрам и списком выборочных дисциплин по семестрам.
    /// </summary>
    /// <param name="id">Уникальный идентификатор учебного плана.</param>
    /// <param name="group">Группа, для которой составлен учебный план.</param>
    /// <param name="semestersSubjects">Словарь, содержащий списки дисциплин по семестрам.</param>
    /// <param name="semestersElectiveSubjects">Словарь, содержащий списки выборочных дисциплин по семестрам.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Curriculum"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Curriculum> Create(Guid id, Group group, Dictionary<uint, List<(Subject, AcademicLoad)>> semestersSubjects, Dictionary<uint, List<(Subject, AcademicLoad)>> semestersElectiveSubjects)
    {
        // Дополнительные валидации, если необходимо

        var curriculum = new Curriculum(id, group, semestersSubjects, semestersElectiveSubjects);
        return Result.Success(curriculum);
    }
}

