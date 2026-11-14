using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

/// <summary>
///     JSON converter that reads and writes <see cref="TimeOnly" /> values as <c>HH:mm</c> (for example
///     <c>10:00</c>), the format used by the frontend. <c>HH:mm:ss</c> is also accepted when reading.
/// </summary>
public class TimeOnlyHourMinuteJsonConverter : JsonConverter<TimeOnly>
{
    private static readonly string[] Formats = ["HH:mm", "HH:mm:ss"];

    /// <inheritdoc />
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value is not null &&
            TimeOnly.TryParseExact(value, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
            return time;

        throw new JsonException($"Invalid time '{value}'. Expected format HH:mm.");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("HH:mm", CultureInfo.InvariantCulture));
    }
}
