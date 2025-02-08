using System.Text.Json.Serialization;

namespace WebApiDotNet6.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TurnoEnum
    {
        Manhã,
        Tarde,
        Noite
    }
}
