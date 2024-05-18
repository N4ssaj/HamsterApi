using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ChairConverter : JsonConverter<Chair>
{
    public override Chair Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var title = root.GetProperty("Title").GetString();
            var teachersIds = JsonSerializer.Deserialize<List<string>>(root.GetProperty("TeachersIds").GetRawText(), options);
            var departmentId = root.GetProperty("DepartmentId").GetString();
            return Chair.Create(id, title, teachersIds, departmentId).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Chair value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteString("Title", value.Title);
        writer.WritePropertyName("TeachersIds");
        JsonSerializer.Serialize(writer, value.TeachersIds, options);
        writer.WriteString("DepartmentId", value.DepartmentId);
        writer.WriteEndObject();
    }
}
