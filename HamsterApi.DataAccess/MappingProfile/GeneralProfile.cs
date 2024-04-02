
using AutoMapper;
using HamsterApi.Core.Models;
using HamsterApi.DataAccess.Entites.Interfaces;

namespace HamsterApi.DataAccess.MappingProfile;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        //Subject
        CreateMap<SubjectEntity, Subject>()
            .ConstructUsing(src => (Subject.Create(src.Id, src.Title, src.Index, src.TeachersIds.ToList())).Value);
        CreateMap<ISubjectEntity, Subject>()
            .ConstructUsing(src => (Subject.Create(src.Id, src.Title, src.Index, src.TeachersIds.ToList())).Value);
        CreateMap<Subject, SubjectEntity>()
            .ConvertUsing(src => new SubjectEntity { Id = src.Id, Title = src.Title, Index = src.Index, TeachersIds = src.TeachersIds.ToList() });
        //Direction
        CreateMap<DirectionEntity,Direction>()
            .ConstructUsing(src=>(Direction.Create(src.Id,src.Title,src.GroupsIds.ToList(),src.FormOfEducation,src.LevelOfEducation,src.DepartmentId)).Value);
        CreateMap<IDirectionEntity, Direction>()
            .ConstructUsing(src => (Direction.Create(src.Id, src.Title, src.GroupsIds.ToList(), src.FormOfEducation, src.LevelOfEducation, src.DepartmentId)).Value);
        CreateMap<Direction,DirectionEntity>()
            .ConstructUsing(src=>new DirectionEntity { Id = src.Id, Title = src.Title,GroupsIds = src.GroupsIds.ToList(),FormOfEducation = src.FormOfEducation, LevelOfEducation = src.LevelOfEducation,DepartmentId = src.DepartmentId });
        //Teacher
        CreateMap<TeacherEntity,Teacher>()
            .ConstructUsing(src=>(Teacher.Create(src.Id, src.Name,src.Surname,src.Patronymic,src.SubjectsIds.ToList(),src.ChairId,src.TeacherLoadId)).Value);
        CreateMap<ITeacherEntity, Teacher>()
            .ConstructUsing(src => (Teacher.Create(src.Id, src.Name, src.Surname, src.Patronymic, src.SubjectsIds.ToList(), src.ChairId, src.TeacherLoadId)).Value);
        CreateMap<Teacher, TeacherEntity>()
            .ConvertUsing(src => new TeacherEntity { Id = src.Id, Name = src.Name, Surname = src.Surname, Patronymic = src.Patronymic, SubjectsIds = src.SubjectsIds.ToList(), ChairId = src.ChairId, TeacherLoadId = src.TeacherLoadId });
        //AcademicLoad
        CreateMap<AcademicLoadEntity, AcademicLoad>()
            .ConstructUsing(src => (AcademicLoad.Create(src.Id, src.Lectures, src.Laboratory, src.Practice, src.Credits,src.AcademicEvaluationType).Value));

        CreateMap<IAcademicLoadEntity, AcademicLoad>()
            .ConstructUsing(src => (AcademicLoad.Create(src.Id, src.Lectures, src.Laboratory, src.Practice, src.Credits, src.AcademicEvaluationType).Value));

        CreateMap<AcademicLoad, AcademicLoadEntity>();

        //Auditorium
        CreateMap<AuditoriumEntity, Auditorium>()
            .ConstructUsing(src => (Auditorium.Create(src.Id, src.Number).Value));

        CreateMap<IAuditoriumEntity, Auditorium>()
            .ConstructUsing(src => (Auditorium.Create(src.Id, src.Number).Value));

        CreateMap<Auditorium, AuditoriumEntity>();

        //Group
        CreateMap<GroupEntity, Group>()
            .ConstructUsing(src => Group.Create(src.Id, src.Number, src.LevelOfEducation, src.DirectionId).Value);
        CreateMap<IGroupEntity, Group>()
            .ConstructUsing(src => Group.Create(src.Id, src.Number, src.LevelOfEducation, src.DirectionId).Value);
        CreateMap<Group, GroupEntity>();

        //TeacherLoad
        CreateMap<TeachingLoadEntity,TeachingLoad>()
            .ConstructUsing(src=>TeachingLoad.Create(src.Id,src.LecturesHours,src.PracticeHours,src.LaboratoryHours,src.LaboratoryHoursMax,src.PracticeHoursMax,src.LaboratoryHoursMax).Value);
        CreateMap<ITeachingLoadEntity, TeachingLoad>()
            .ConstructUsing(src => TeachingLoad.Create(src.Id, src.LecturesHours, src.PracticeHours, src.LaboratoryHours, src.LaboratoryHoursMax, src.PracticeHoursMax, src.LaboratoryHoursMax).Value);
        CreateMap<TeachingLoad, ITeachingLoadEntity>();
    }
}
