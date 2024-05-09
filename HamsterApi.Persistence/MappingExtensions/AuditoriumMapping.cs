

using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class AuditoriumMapping
{
    public static IAuditoriumEntity ToEnity(this Auditorium auditorium)
    {
        IAuditoriumEntity auditoriumEntity = new AuditoriumEntity
        {
            Id = auditorium.Id,
            Number = auditorium.Number
        };

        return auditoriumEntity;
    }

    public static Auditorium ToModel(this IAuditoriumEntity auditoriumEntity)
    {
        Auditorium auditorium = Auditorium.Create(auditoriumEntity.Id,
            auditoriumEntity.Number).Value;

        return auditorium;
    }
}
