
using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;
//нужен год приема
/// <summary>
/// Представляет учебный план для определенной группы.
/// </summary>
public class Curriculum
{
    /// <summary>
    /// Инициализирует новый учебный план с указанным идентификатором, группой, списком дисциплин по семестрам и списком выборочных дисциплин по семестрам.
    /// </summary>
    /// <param name="id">Уникальный идентификатор учебного плана.</param>
    /// <param name="direction">Группа, для которой составлен учебный план.</param>
    /// <param name="semestersSubjects">Словарь, содержащий списки дисциплин по семестрам.</param>
    /// <param name="semestersElectiveSubjects">Словарь, содержащий списки выборочных дисциплин по семестрам.</param>
    private Curriculum(string id , string directionId,  IReadOnlyCollection<SubjectWtihLoad> semestersSubjects,  IReadOnlyCollection<SubjectWtihLoad> semestersElectiveSubjects, int yearOfPreparation,string fGOSNumber)
        => (Id, DirectionId, SemestersSubjects, SemestersSubjects,YearOfPreparation,FGOSNumber) 
        = (id, directionId, semestersSubjects, semestersElectiveSubjects,yearOfPreparation,fGOSNumber);

    /// <summary>
    /// Получает уникальный идентификатор учебного плана.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Получает направление, для которой составлен учебный план.
    /// </summary>
    public string DirectionId { get; }

    public int YearOfPreparation { get; }

    public string FGOSNumber { get; }
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
    /// <param name="direction">Группа, для которой составлен учебный план.</param>
    /// <param name="semestersSubjects">Словарь, содержащий списки дисциплин по семестрам.</param>
    /// <param name="semestersElectiveSubjects">Словарь, содержащий списки выборочных дисциплин по семестрам.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Curriculum"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Curriculum> Create(string id , string directionId,IReadOnlyCollection<SubjectWtihLoad> semestersSubjects,IReadOnlyCollection<SubjectWtihLoad> semestersElectiveSubjects, int yearOfPreparation, string fGOSNumber)
    {
        // Дополнительные валидации, если необходимо

        var curriculum = new Curriculum(id, directionId, semestersSubjects, semestersElectiveSubjects,yearOfPreparation,fGOSNumber);
        return curriculum;
    }
}

