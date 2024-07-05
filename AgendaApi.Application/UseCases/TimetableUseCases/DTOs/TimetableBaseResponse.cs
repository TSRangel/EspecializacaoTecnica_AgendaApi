using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgendaApi.Application.UseCases.TimetableUseCases.DTOs
{
    public abstract record TimetableBaseResponse
    {
        public Guid TimetableId { get; set; }
        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly StartTime { get; set; }
        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly EndTime { get; set; }
        public Guid LegalEntityId { get; set; }
        public int WeekDayId { get; set; }
    }

    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private const string TimeFormat = "HH:mm";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => TimeOnly.ParseExact(reader.GetString()!, "HH:mm", CultureInfo.InvariantCulture);

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(TimeFormat));
        }
    }
}