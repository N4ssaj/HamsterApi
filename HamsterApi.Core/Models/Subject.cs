

using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет учебную дисциплину.
/// </summary>
public class Subject
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Subject"/> с указанным идентификатором, названием, индексом и списком преподавателей.
    /// </summary>
    /// <param name="id">Уникальный идентификатор дисциплины.</param>
    /// <param name="title">Название дисциплины.</param>
    /// <param name="index">Индекс дисциплины.</param>
    /// <param name="teachers">Список преподавателей, преподающих эту дисциплину.</param>
    private Subject(string id , string title, string index, IReadOnlyCollection<Teacher> teachers)
        => (Id, Title, Index, Teachers) = (id, title, index, teachers);

    /// <summary>
    /// Получает название дисциплины.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Получает индекс дисциплины.
    /// </summary>
    public string Index { get; }

    /// <summary>
    /// Получает список преподавателей, преподающих эту дисциплину.
    /// </summary>
    public IReadOnlyCollection<Teacher> Teachers { get; }

    /// <summary>
    /// Получает уникальный идентификатор дисциплины.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="Subject"/> с указанным идентификатором, названием, индексом и списком преподавателей.
    /// </summary>
    /// <param name="id">Уникальный идентификатор дисциплины.</param>
    /// <param name="title">Название дисциплины.</param>
    /// <param name="index">Индекс дисциплины.</param>
    /// <param name="teachers">Список преподавателей, преподающих эту дисциплину.</param>
    /// <returns>Результат, указывающий на успешность создания экземпляра класса <see cref="Subject"/> и содержащий созданный объект, если операция выполнена успешно.</returns>
    public static Result<Subject> Create(string id , string title, string index, IReadOnlyCollection<Teacher> teachers)
    {
        // Дополнительные валидации, если необходимо

        var subject = new Subject(id, title, index, teachers);

        return subject;
    }
}
