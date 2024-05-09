
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class TeacherMapping
{
    public static ITeacherEntity ToEntity(this Teacher teacher)
    {
        ITeacherEntity teacherEntity = new TeacherEntity
        {
            Id = teacher.Id,
            ChairId = teacher.ChairId,
            FullName = teacher.FullName,
            Name = teacher.Name,
            Patronymic = teacher.Patronymic,
            SubjectsIds = teacher.SubjectsIds.ToList(),
            Surname = teacher.Surname,
            TeacherLoadId = teacher.TeacherLoadId
        };

        return teacherEntity;
    }

    public static Teacher ToModel(this ITeacherEntity teacherEntity)
    {
        Teacher teacher = Teacher.Create(teacherEntity.Id,
            teacherEntity.Name,
            teacherEntity.Surname,
            teacherEntity.Patronymic,
            teacherEntity.SubjectsIds.ToList(),
            teacherEntity.ChairId,
            teacherEntity.TeacherLoadId).Value;

        return teacher;
    }
}
