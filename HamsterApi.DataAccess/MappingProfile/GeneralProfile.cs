﻿
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
            .ConstructUsing(src => (Subject.Create(src.Id, src.Title, src.Index, src.TeachersIds.ToList().AsReadOnly())).Value);
        CreateMap<ISubjectEntity, Subject>()
            .ConstructUsing(src => (Subject.Create(src.Id, src.Title, src.Index, src.TeachersIds.ToList().AsReadOnly())).Value);
        CreateMap<Subject, SubjectEntity>()
            .ConvertUsing(src => new SubjectEntity { Id = src.Id, Title = src.Title, Index = src.Index, TeachersIds = src.TeachersIds.ToList() });

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
