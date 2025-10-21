using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Server.Dtos
{
  public class DateOnlyJsonConverter : JsonConverter<DateOnly>
  {
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var str = reader.GetString();
      if (string.IsNullOrWhiteSpace(str))
        throw new JsonException("Date string is null or empty.");

      // Try to split and get only the date part
      var datePart = str.Split(' ')[0].TrimEnd('.');
      return DateOnly.Parse(datePart);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
  }
}
