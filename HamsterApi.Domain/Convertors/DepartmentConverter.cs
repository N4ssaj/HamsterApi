using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DepartmentConverter : JsonConverter<Department>
{
    public override Department Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var title = root.GetProperty("Title").GetString();
            var chairsIds = JsonSerializer.Deserialize<List<string>>(root.GetProperty("ChairsIds").GetRawText(), options);
            var directionsIds = JsonSerializer.Deserialize<List<string>>(root.GetProperty("DirectionsIds").GetRawText(), options);
            return Department.Create(id, title, chairsIds, directionsIds).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Department value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteString("Title", value.Title);
        writer.WritePropertyName("ChairsIds");
        JsonSerializer.Serialize(writer, value.ChairsIds, options);
        writer.WritePropertyName("DirectionsIds");
        JsonSerializer.Serialize(writer, value.DirectionsIds, options);
        writer.WriteEndObject();
    }
}
