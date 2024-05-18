using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ScheduleConverter : JsonConverter<Schedule>
{
    public override Schedule Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var year = root.GetProperty("Year").GetInt32();
            var springOrAutumn = JsonSerializer.Deserialize<SpringOrAutumn>(root.GetProperty("SpringOrAutumn").GetRawText(), options);
            var groupsScheduleIds = JsonSerializer.Deserialize<List<string>>(root.GetProperty("GroupsScheduleIds").GetRawText(), options);
            return Schedule.Create(id, year, springOrAutumn, groupsScheduleIds).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Schedule value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteNumber("Year", value.Year);
        writer.WritePropertyName("SpringOrAutumn");
        JsonSerializer.Serialize(writer, value.SpringOrAutumn, options);
        writer.WritePropertyName("GroupsScheduleIds");
        JsonSerializer.Serialize(writer, value.GroupsScheduleIds, options);
        writer.WriteEndObject();
    }
}
