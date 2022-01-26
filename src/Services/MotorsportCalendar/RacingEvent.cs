using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace chimibot.Services.MotorsportCalendar;

public record RacingEvent
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("location")]
    public RacingEventLocation RacingEventLocation { get; set; }

    [JsonPropertyName("series")]
    public List<RacingSeries> Series { get; set; }
}

public record RacingSeries
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public record RacingEventLocation
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}
