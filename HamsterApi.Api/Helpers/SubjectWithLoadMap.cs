using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Helpers;

internal static class SubjectWithLoadMap
{
    internal static SubjectWtihLoad Map(this SubjectWtihLoadRequest request)
    {
        Subject subject=Subject.Create(request.Subject.Id,
            request.Subject.Title,
            request.Subject.Index,
            request.Subject.TeachersIds).Value;
        AcademicLoad academic = AcademicLoad.Create(request.AcademicLoad.Id, 
            request.AcademicLoad.Lectures, 
            request.AcademicLoad.Laboratory, 
            request.AcademicLoad.Practice, 
            request.AcademicLoad.Credits, 
            request.AcademicLoad.AcademicEvaluationType).Value;
        SubjectWtihLoad subjectWtihLoad=SubjectWtihLoad.Create(Guid.NewGuid().ToString(),subject,academic).Value;

        return subjectWtihLoad;
    }
    internal static SubjectWtihLoadResponse Map(this SubjectWtihLoad subjectWtihLoad)
    {
        SubjectResponse subjectResponse=new SubjectResponse(subjectWtihLoad.Subject.Id,
            subjectWtihLoad.Subject.Title,
            subjectWtihLoad.Subject.Index,
            subjectWtihLoad.Subject.TeachersIds.ToList());
        AcademicLoadResponse academicLoadResponse = new AcademicLoadResponse(subjectWtihLoad.AcademicLoad.Id,
            subjectWtihLoad.AcademicLoad.Lectures,
            subjectWtihLoad.AcademicLoad.Laboratory,
            subjectWtihLoad.AcademicLoad.Practice,
            subjectWtihLoad.AcademicLoad.Credits,
            subjectWtihLoad.AcademicLoad.Total,
            subjectWtihLoad.AcademicLoad.AcademicEvaluationType);

        SubjectWtihLoadResponse response = new SubjectWtihLoadResponse(subjectWtihLoad.Id, subjectResponse, academicLoadResponse);
        return response;
    }
}
