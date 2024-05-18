using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SubjectWtihLoadConverter : JsonConverter<SubjectWtihLoad>
{
    private readonly JsonSerializerOptions _subjectOptions;
    private readonly JsonSerializerOptions _academicLoadOptions;

    public SubjectWtihLoadConverter()
    {
        _subjectOptions = new JsonSerializerOptions
        {
            Converters = { new SubjectConverter() }
        };
        _academicLoadOptions = new JsonSerializerOptions
        {
            Converters = { new AcademicLoadConverter() }
        };
    }

    public override SubjectWtihLoad Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var subject = JsonSerializer.Deserialize<Subject>(root.GetProperty("Subject").GetRawText(), _subjectOptions);
            var academicLoad = JsonSerializer.Deserialize<AcademicLoad>(root.GetProperty("AcademicLoad").GetRawText(), _academicLoadOptions);
            return SubjectWtihLoad.Create(id, subject, academicLoad).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, SubjectWtihLoad value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WritePropertyName("Subject");
        JsonSerializer.Serialize(writer, value.Subject, _subjectOptions);
        writer.WritePropertyName("AcademicLoad");
        JsonSerializer.Serialize(writer, value.AcademicLoad, _academicLoadOptions);
        writer.WriteEndObject();
    }
}
