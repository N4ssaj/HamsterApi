namespace HamsterApi.Api.Controllers;

public record TeacherResponse(string Id,string Name, string Surname, string Patronymic, IReadOnlyCollection<string> SubjectsIds, string ChairId, string TeacherLoadId);