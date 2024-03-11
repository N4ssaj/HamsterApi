using CSharpFunctionalExtensions;

namespace HamsterApi.Core.Models;

/// <summary>
/// Представляет группу в учебном заведении.
/// </summary>
public class Group
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Group"/> с указанным идентификатором и номером.
    /// </summary>
    /// <param name="id">Уникальный идентификатор группы.</param>
    /// <param name="number">Номер/код, назначенный группе.</param>
    private Group(Guid id, string number)
        => (Id, Number) = (id, number);

    /// <summary>
    /// Получает номер/код, назначенный группе.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Получает уникальный идентификатор группы.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="Group"/> с указанным идентификатором и номером.
    /// </summary>
    /// <param name="id">Уникальный идентификатор группы.</param>
    /// <param name="number">Номер/код, назначенный группе.</param>
    /// <returns>Результат, указывающий, было ли создание успешным, и содержащий созданную группу в случае успеха.</returns>
    public static Result<Group> Create(Guid id, string number)
    {
        // валидация
        var group = new Group(id, number);
        return Result.Success(group);
    }
}
