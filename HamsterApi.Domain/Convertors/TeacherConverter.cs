using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TeacherConverter : JsonConverter<Teacher>
{
    public override Teacher Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var name = root.GetProperty("Name").GetString();
            var surname = root.GetProperty("Surname").GetString();
            var patronymic = root.GetProperty("Patronymic").GetString();
            var subjectsIds = JsonSerializer.Deserialize<List<string>>(root.GetProperty("SubjectsIds").GetRawText(), options);
            var chairId = root.GetProperty("ChairId").GetString();
            var teacherLoadId = root.GetProperty("TeacherLoadId").GetString();
            return Teacher.Create(id, name, surname, patronymic, subjectsIds, chairId, teacherLoadId).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, Teacher value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteString("Name", value.Name);
        writer.WriteString("Surname", value.Surname);
        writer.WriteString("Patronymic", value.Patronymic);
        writer.WriteString("FullName", value.FullName);
        writer.WritePropertyName("SubjectsIds");
        JsonSerializer.Serialize(writer, value.SubjectsIds, options);
        writer.WriteString("ChairId", value.ChairId);
        writer.WriteString("TeacherLoadId", value.TeacherLoadId);
        writer.WriteEndObject();
    }
}
