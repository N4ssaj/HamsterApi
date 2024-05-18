using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SemesterConverter : JsonConverter<Semester>
{
    public override Semester Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var number = root.GetProperty("Number").GetInt32();
            var groupId = root.GetProperty("GroupId").GetString();
            var subjects = JsonSerializer.Deserialize<List<SubjectWtihLoad>>(root.GetProperty("Subjects").GetRawText(), options);
            return Semester.Create(id, number, groupId, subjects).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Semester value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteNumber("Number", value.Number);
        writer.WriteString("GroupId", value.GroupId);
        writer.WritePropertyName("Subjects");
        JsonSerializer.Serialize(writer, value.Subjects, options);
        writer.WriteEndObject();
    }
}
