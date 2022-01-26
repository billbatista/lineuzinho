using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace chimibot.Services.MotorsportCalendar;

public class MotorsportCalendarService
{
    private const string JsonFilePath = "calendar.json";

    public static async Task<List<RacingEvent>> GetOngoing()
    {
        var events = await GetAllData();
        return events
            .Where(IsEventOngoing).ToList()
            .OrderBy(x => x.StartDate).ToList();
    }

    public static async Task<List<RacingEvent>> GetUpcomingEvents()
    {
        var cutoffDate = DateTime.UtcNow.Date.AddDays(7);
        var events = await GetAllData();
        return events
            .Where(x =>
                x.StartDate < cutoffDate &&
                x.StartDate > DateTime.UtcNow.Date)
            .OrderBy(x => x.StartDate).ToList();
    }

    private static async Task<List<RacingEvent>> GetAllData()
    {
        await using (var jsonFile = File.OpenRead(JsonFilePath))
        {
            return await JsonSerializer.DeserializeAsync<List<RacingEvent>>(jsonFile);
        }
    }

    private static bool IsEventOngoing(RacingEvent @event)
    {
        var eventDateRange = GetDateRange(@event.StartDate, @event.EndDate).ToList();
        if (eventDateRange.Any(x => x.Date == DateTime.UtcNow.Date)) return true;

        return false;
    }

    private static IEnumerable<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
    {
        while (startDate.Date <= endDate.Date)
        {
            yield return startDate;
            startDate = startDate.AddDays(1);
        }
    }
}
