using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class AuditoriumConverter : JsonConverter<Auditorium>
{
    public override Auditorium Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var number = root.GetProperty("Number").GetString();
            return Auditorium.Create(id, number).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Auditorium value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteString("Number", value.Number);
        writer.WriteEndObject();
    }
}
