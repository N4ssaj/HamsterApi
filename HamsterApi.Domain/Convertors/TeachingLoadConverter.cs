using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TeachingLoadConverter : JsonConverter<TeachingLoad>
{
    public override TeachingLoad Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var lecturesHours = root.GetProperty("LecturesHours").GetInt32();
            var practiceHours = root.GetProperty("PracticeHours").GetInt32();
            var laboratoryHours = root.GetProperty("LaboratoryHours").GetInt32();
            var lecturesHoursMax = root.GetProperty("LecturesHoursMax").GetInt32();
            var practiceHoursMax = root.GetProperty("PracticeHoursMax").GetInt32();
            var laboratoryHoursMax = root.GetProperty("LaboratoryHoursMax").GetInt32();
            return TeachingLoad.Create(id, lecturesHours, practiceHours, laboratoryHours, lecturesHoursMax, practiceHoursMax, laboratoryHoursMax).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, TeachingLoad value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteNumber("LecturesHours", value.LecturesHours);
        writer.WriteNumber("PracticeHours", value.PracticeHours);
        writer.WriteNumber("LaboratoryHours", value.LaboratoryHours);
        writer.WriteNumber("LecturesHoursMax", value.LecturesHoursMax);
        writer.WriteNumber("PracticeHoursMax", value.PracticeHoursMax);
        writer.WriteNumber("LaboratoryHoursMax", value.LaboratoryHoursMax);
        writer.WriteEndObject();
    }
}
