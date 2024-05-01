
using AutoMapper;
using HamsterApi.Core.Models;
using HamsterApi.DataAccess.Entites.Interfaces;
using VDS.RDF.Query;

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
        //Department
        CreateMap<DepartmentEntity, Department>()
            .ConstructUsing(src => (Department.Create(src.Id, src.Title, src.ChairsIds.ToList(), src.DirectionsIds.ToList()).Value));
        CreateMap<IDepartmentEntity, Department>()
            .ConstructUsing(src => (Department.Create(src.Id, src.Title, src.ChairsIds.ToList(), src.DirectionsIds.ToList()).Value));
        CreateMap<Department, DepartmentEntity>()
            .ConvertUsing(src => new DepartmentEntity { Id=src.Id,Title=src.Title,ChairsIds=src.ChairsIds.ToList(),DirectionsIds=src.DirectionsIds.ToList()});
        //Semester
        CreateMap<SemesterEntity, Semester>()
            .ConstructUsing(src => (Semester.Create(src.Id, src.Number, src.GroupId, src.Subjects.Select(i=>SubjectWtihLoad.Create(i.Id,Subject.Create(i.Subject.Id,i.Subject.Title,i.Subject.Index,i.Subject.TeachersIds.ToList()).Value,AcademicLoad.Create(i.AcademicLoad.Id,i.AcademicLoad.Lectures,i.AcademicLoad.Laboratory,i.AcademicLoad.Practice,i.AcademicLoad.Credits,i.AcademicLoad.AcademicEvaluationType).Value,i.SemesterNumber).Value).ToList()).Value));
        CreateMap<ISemesterEntity, Semester>()
            .ConstructUsing(src => (Semester.Create(src.Id, src.Number, src.GroupId, src.Subjects.Select(i => SubjectWtihLoad.Create(i.Id, Subject.Create(i.Subject.Id, i.Subject.Title, i.Subject.Index, i.Subject.TeachersIds.ToList()).Value, AcademicLoad.Create(i.AcademicLoad.Id, i.AcademicLoad.Lectures, i.AcademicLoad.Laboratory, i.AcademicLoad.Practice, i.AcademicLoad.Credits, i.AcademicLoad.AcademicEvaluationType).Value, i.SemesterNumber).Value).ToList()).Value));
        CreateMap<Semester, SemesterEntity>()
            .ConstructUsing(src => new SemesterEntity { Id = src.Id, Number = src.Number, GroupId = src.GroupId, Subjects = src.Subjects.Select(i=>(ISubjectWtihLoadEntity)new SubjectWtihLoadEntity { Id = i.Id, SemesterNumber = i.SemesterNumber, AcademicLoad = new AcademicLoadEntity { Id = i.AcademicLoad.Id, Lectures = i.AcademicLoad.Lectures, Credits = i.AcademicLoad.Credits, Laboratory = i.AcademicLoad.Laboratory, Practice = i.AcademicLoad.Practice, Total = i.AcademicLoad.Total, AcademicEvaluationType = i.AcademicLoad.AcademicEvaluationType }, Subject = new SubjectEntity { Id = i.Subject.Id, Index = i.Subject.Index, TeachersIds = i.Subject.TeachersIds.ToList(), Title = i.Subject.Title } } ).ToList()});
        //Chair
        CreateMap<ChairEntity, Chair>()
            .ConstructUsing(src => (Chair.Create(src.Id, src.Title, src.TeachersIds.ToList(), src.DepartmentId).Value));
        CreateMap<IChairEntity, Chair>()
            .ConstructUsing(src => (Chair.Create(src.Id, src.Title, src.TeachersIds.ToList(), src.DepartmentId).Value));
        CreateMap<Chair, ChairEntity>()
            .ConstructUsing(src=>new ChairEntity { Id=src.Id,Title=src.Title,TeachersIds=src.TeachersIds.ToList(),DepartmentId=src.DepartmentId });
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
