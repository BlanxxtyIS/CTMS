namespace TaskManagment.Core.Entities;

/// <summary>
/// Базовый класс для всех сущностей в системе.
/// Обеспечиывает общие свойства, такие как уникальный идентификатор и временные данные.
/// </summary>
public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
