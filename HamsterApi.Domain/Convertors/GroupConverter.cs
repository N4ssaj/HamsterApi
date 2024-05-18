using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class GroupConverter : JsonConverter<Group>
{
    public override Group Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var number = root.GetProperty("Number").GetString();
            var levelOfEducation = JsonSerializer.Deserialize<LevelOfEducation>(root.GetProperty("LevelOfEducation").GetRawText(), options);
            var directionId = root.GetProperty("DirectionId").GetString();
            return Group.Create(id, number, levelOfEducation, directionId).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Group value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteString("Number", value.Number);
        writer.WritePropertyName("LevelOfEducation");
        JsonSerializer.Serialize(writer, value.LevelOfEducation, options);
        writer.WriteString("DirectionId", value.DirectionId);
        writer.WriteEndObject();
    }
}
