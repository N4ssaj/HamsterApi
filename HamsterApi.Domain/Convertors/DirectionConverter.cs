using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DirectionConverter : JsonConverter<Direction>
{
    public override Direction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var title = root.GetProperty("Title").GetString();
            var groupsIds = JsonSerializer.Deserialize<List<string>>(root.GetProperty("GroupsIds").GetRawText(), options);
            var formOfEducation = JsonSerializer.Deserialize<FormOfEducation>(root.GetProperty("FormOfEducation").GetRawText(), options);
            var levelOfEducation = JsonSerializer.Deserialize<LevelOfEducation>(root.GetProperty("LevelOfEducation").GetRawText(), options);
            var departmentId = root.GetProperty("DepartmentId").GetString();
            return Direction.Create(id, title, groupsIds, formOfEducation, levelOfEducation, departmentId).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Direction value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteString("Title", value.Title);
        writer.WritePropertyName("GroupsIds");
        JsonSerializer.Serialize(writer, value.GroupsIds, options);
        writer.WritePropertyName("FormOfEducation");
        JsonSerializer.Serialize(writer, value.FormOfEducation, options);
        writer.WritePropertyName("LevelOfEducation");
        JsonSerializer.Serialize(writer, value.LevelOfEducation, options);
        writer.WriteString("DepartmentId", value.DepartmentId);
        writer.WriteEndObject();
    }
}
