namespace HamsterApi.Api.Contracts.Request;

public record TeacherRequest(string Name, string Surname, string Patronymic, List<string> SubjectsIds, string ChairId, string TeacherLoadId);