using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class AcademicLoadConverter : JsonConverter<AcademicLoad>
{
    public override AcademicLoad Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var root = jsonDoc.RootElement;
            var id = root.GetProperty("Id").GetString();
            var lectures = root.GetProperty("Lectures").GetInt32();
            var laboratory = root.GetProperty("Laboratory").GetInt32();
            var practice = root.GetProperty("Practice").GetInt32();
            var credits = root.GetProperty("Credits").GetInt32();
            var academicEvaluationType = JsonSerializer.Deserialize<AcademicEvaluationType>(root.GetProperty("AcademicEvaluationType").GetRawText(), options);
            return AcademicLoad.Create(id, lectures, laboratory, practice, credits, academicEvaluationType).Value;
        }
    }

    public override void Write(Utf8JsonWriter writer, AcademicLoad value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id);
        writer.WriteNumber("Lectures", value.Lectures);
        writer.WriteNumber("Laboratory", value.Laboratory);
        writer.WriteNumber("Practice", value.Practice);
        writer.WriteNumber("Credits", value.Credits);
        writer.WriteNumber("Total", value.Total);
        writer.WritePropertyName("AcademicEvaluationType");
        JsonSerializer.Serialize(writer, value.AcademicEvaluationType, options);
        writer.WriteEndObject();
    }
}
