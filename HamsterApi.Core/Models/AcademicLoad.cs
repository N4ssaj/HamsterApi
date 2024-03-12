using HamsterApi.Core.Common;
using HamsterApi.Core.Common.Enum;


namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет академическую нагрузку для учебной дисциплины.
/// </summary>
public class AcademicLoad
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AcademicLoad"/> с указанным идентификатором и параметрами нагрузки.
    /// </summary>
    /// <param name="id">Уникальный идентификатор академической нагрузки.</param>
    /// <param name="lectures">Количество лекционных занятий в неделю.</param>
    /// <param name="laboratory">Количество лабораторных занятий в неделю.</param>
    /// <param name="practice">Количество практических занятий в неделю.</param>
    /// <param name="controlWork">Количество контрольных работ в семестр.</param>
    /// <param name="independentWork">Количество самостоятельной работы в неделю.</param>
    /// <param name="credits">Количество зачетных единиц.</param>
    /// <param name="total">Общее количество академической нагрузки.</param>
    /// <param name="academicEvaluationType">Тип оценивания академической нагрузки.</param>
    private AcademicLoad(string id, int lectures, int laboratory, int practice, int controlWork, int independentWork, int credits, int total, AcademicEvaluationType academicEvaluationType)
        => (Id, Lectures, Laboratory, Practice, ControlWork, IndependentWork, Credits, Total, AcademicEvaluationType)
            = (id, lectures, laboratory, practice, controlWork, independentWork, credits, total, academicEvaluationType);

    /// <summary>
    /// Получает уникальный идентификатор академической нагрузки.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Получает количество лекционных занятий в неделю.
    /// </summary>
    public int Lectures { get; }

    /// <summary>
    /// Получает количество лабораторных занятий в неделю.
    /// </summary>
    public int Laboratory { get; }

    /// <summary>
    /// Получает количество практических занятий в неделю.
    /// </summary>
    public int Practice { get; }

    /// <summary>
    /// Получает количество контрольных работ в семестр.
    /// </summary>
    public int ControlWork { get; }

    /// <summary>
    /// Получает количество самостоятельной работы в неделю.
    /// </summary>
    public int IndependentWork { get; }

    /// <summary>
    /// Получает количество зачетных единиц.
    /// </summary>
    public int Credits { get; }

    /// <summary>
    /// Получает общее количество академической нагрузки.
    /// </summary>
    public int Total { get; }

    /// <summary>
    /// Получает тип оценивания академической нагрузки.
    /// </summary>
    public AcademicEvaluationType AcademicEvaluationType { get; }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="AcademicLoad"/> с указанными параметрами нагрузки.
    /// </summary>
    /// <param name="id">Уникальный идентификатор академической нагрузки.</param>
    /// <param name="lectures">Количество лекционных занятий в неделю.</param>
    /// <param name="laboratory">Количество лабораторных занятий в неделю.</param>
    /// <param name="practice">Количество практических занятий в неделю.</param>
    /// <param name="controlWork">Количество контрольных работ в семестр.</param>
    /// <param name="independentWork">Количество самостоятельной работы в неделю.</param>
    /// <param name="credits">Количество зачетных единиц.</param>
    /// <param name="academicEvaluationType">Тип оценивания академической нагрузки.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="AcademicLoad"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<AcademicLoad> Create(string id, int lectures, int laboratory, int practice, int controlWork, int independentWork, int credits, AcademicEvaluationType academicEvaluationType)
    {
        // валидация
        var total = lectures + laboratory + practice + controlWork + independentWork + credits;
        var academicLoad = new AcademicLoad(id, lectures, laboratory, practice, controlWork, independentWork, credits, total, academicEvaluationType);
        return academicLoad;
    }
}
