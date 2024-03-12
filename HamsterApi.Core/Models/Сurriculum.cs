
using HamsterApi.Core.Common;

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
    private Curriculum(string id , Group group,  IReadOnlyCollection<SubjectWtihLoad> semestersSubjects,  IReadOnlyCollection<SubjectWtihLoad> semestersElectiveSubjects)
        => (Id, Group, SemestersSubjects, SemestersSubjects) = (id, group, semestersSubjects, semestersElectiveSubjects);

    /// <summary>
    /// Получает уникальный идентификатор учебного плана.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Получает группу, для которой составлен учебный план.
    /// </summary>
    public Group Group { get; }

    /// <summary>
    /// Получает словарь, содержащий списки дисциплин по семестрам.
    /// </summary>
    public IReadOnlyCollection<SubjectWtihLoad> SemestersSubjects { get; }

    /// <summary>
    /// Получает словарь, содержащий списки выборочных дисциплин по семестрам.
    /// </summary>
    public  IReadOnlyCollection<SubjectWtihLoad> SemestersElectiveSubjects { get; }

    /// <summary>
    /// Создает новый учебный план с указанным идентификатором, группой, списком дисциплин по семестрам и списком выборочных дисциплин по семестрам.
    /// </summary>
    /// <param name="id">Уникальный идентификатор учебного плана.</param>
    /// <param name="group">Группа, для которой составлен учебный план.</param>
    /// <param name="semestersSubjects">Словарь, содержащий списки дисциплин по семестрам.</param>
    /// <param name="semestersElectiveSubjects">Словарь, содержащий списки выборочных дисциплин по семестрам.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Curriculum"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Curriculum> Create(string id , Group group,IReadOnlyCollection<SubjectWtihLoad> semestersSubjects,IReadOnlyCollection<SubjectWtihLoad> semestersElectiveSubjects)
    {
        // Дополнительные валидации, если необходимо

        var curriculum = new Curriculum(id, group, semestersSubjects, semestersElectiveSubjects);
        return curriculum;
    }
}

