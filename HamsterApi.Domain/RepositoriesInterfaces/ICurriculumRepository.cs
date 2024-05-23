﻿

using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces.Base;

namespace HamsterApi.Domain.RepositoriesInterfaces;

public interface ICurriculumRepository:IBaseRepository<Curriculum>
{
    public Task<bool> Update(string id, string chairId, string departmentId, string directionId, List<SubjectWtihLoad> semestersSubjects, List<SubjectWtihLoad> semestersElectiveSubjects, int yearOfPreparation, string fGOSNumber);
    public Task<bool> AddElectives(string id, IEnumerable<SubjectWtihLoad> subjectWtihLoads);
    public Task<bool> RemoveElectives(string id, IEnumerable<string> subjectIds);
    public Task<bool> AddSubject(string id, IEnumerable<SubjectWtihLoad> subjectWtihLoads);
    public Task<bool> RemoveSubject(string id, IEnumerable<string> subjectIds);

}