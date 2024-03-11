
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет аудиторию в учебном заведении.
/// </summary>
public class Auditorium
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Auditorium"/> с указанным идентификатором и номером.
    /// </summary>
    /// <param name="id">Уникальный идентификатор аудитории.</param>
    /// <param name="number">Номер/название аудитории.</param>
    private Auditorium(Guid id, string number)
        => (Id, Number) = (id, number);

    /// <summary>
    /// Получает номер/название аудитории.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Получает уникальный идентификатор аудитории.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="Auditorium"/> с указанным идентификатором и номером.
    /// </summary>
    /// <param name="id">Уникальный идентификатор аудитории.</param>
    /// <param name="number">Номер/название аудитории.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Auditorium"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Auditorium> Create(Guid id, string number)
    {
        /*if (!Regex.Match(number, @"^\d{1}-\d{3}[а-я]?").Success)
            return Result.Failure<Auditorium>("не правильный формат названия аудитории");*/
        var auditorium = new Auditorium(id, number);
        return Result.Success(auditorium);
    }
}
