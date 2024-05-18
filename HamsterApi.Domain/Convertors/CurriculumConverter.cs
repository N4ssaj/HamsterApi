using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CurriculumConverter : JsonConverter<Curriculum>
{
    public override Curriculum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var directionId = root.GetProperty("DirectionId").GetString();
            var chairId = root.GetProperty("ChairId").GetString();
            var departmentId = root.GetProperty("DepartmentId").GetString();
            var semestersSubjects = JsonSerializer.Deserialize<List<SubjectWtihLoad>>(root.GetProperty("SemestersSubjects").GetRawText(), options);
            var semestersElectiveSubjects = JsonSerializer.Deserialize<List<SubjectWtihLoad>>(root.GetProperty("SemestersElectiveSubjects").GetRawText(), options);
            var yearOfPreparation = root.GetProperty("YearOfPreparation").GetInt32();
            var fGOSNumber = root.GetProperty("FGOSNumber").GetString();
            return Curriculum.Create(id, chairId, departmentId, directionId, semestersSubjects, semestersElectiveSubjects, yearOfPreparation, fGOSNumber).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Curriculum value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteString("DirectionId", value.DirectionId);
        writer.WriteString("ChairId", value.ChairId);
        writer.WriteString("DepartmentId", value.DepartmentId);
        writer.WriteNumber("YearOfPreparation", value.YearOfPreparation);
        writer.WriteString("FGOSNumber", value.FGOSNumber);
        writer.WritePropertyName("SemestersSubjects");
        JsonSerializer.Serialize(writer, value.SemestersSubjects, options);
        writer.WritePropertyName("SemestersElectiveSubjects");
        JsonSerializer.Serialize(writer, value.SemestersElectiveSubjects, options);
        writer.WriteEndObject();
    }
}
