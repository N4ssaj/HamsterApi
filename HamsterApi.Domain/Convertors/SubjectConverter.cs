using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SubjectConverter : JsonConverter<Subject>
{
    public override Subject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var title = root.GetProperty("Title").GetString();
            var index = root.GetProperty("Index").GetString();
            var teachersIds = JsonSerializer.Deserialize<List<string>>(root.GetProperty("TeachersIds").GetRawText(), options);
            return Subject.Create(id, title, index, teachersIds).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Subject value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteString("Title", value.Title);
        writer.WriteString("Index", value.Index);
        writer.WritePropertyName("TeachersIds");
        JsonSerializer.Serialize(writer, value.TeachersIds, options);
        writer.WriteEndObject();
    }
}
