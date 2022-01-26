using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace chimibot.Modules;

using Services.MotorsportCalendar;

public class RacingModule : ModuleBase<SocketCommandContext>
{
    private readonly MotorsportCalendarService motorsportCalendarService;

    public RacingModule(MotorsportCalendarService motorsportCalendarService) => this.motorsportCalendarService = motorsportCalendarService;

    [Command("racing")]
    public async Task Racing(params string[] args)
    {
        if (args.Length > 1) await ReplyAsync("I don't know how to do that");

        if (args.Length == 0)
        {
            var events = MotorsportCalendarService.GetOngoing().GetAwaiter().GetResult();
            var anyEvents = events.Count > 0;
            var answer = new EmbedBuilder
            {
                Title = anyEvents ? $"We have {events.Count} events currently ongoing" : "No events found",
                Color = anyEvents ? Color.Green : Color.DarkGrey,
            };
            SetFieldForEventsList(events, answer);

            await ReplyAsync(embed: answer.Build());
        }

        if (args[0].Equals("upcoming"))
        {
            var events = MotorsportCalendarService.GetUpcomingEvents().GetAwaiter().GetResult();
            var anyEvents = events.Count > 0;
            var answer = new EmbedBuilder
            {
                Title = anyEvents ? $"We have {events.Count} upcoming events :race_car:" : "No events found",
                Color = anyEvents ? Color.Green : Color.DarkGrey,
            };
            SetFieldForEventsList(events, answer);

            await ReplyAsync(embed: answer.Build());
        }

        await Task.CompletedTask;
    }

    private static void SetFieldForEventsList(List<RacingEvent> events, EmbedBuilder answer)
    {
        foreach (var @event in events)
        {
            answer.AddField($"{@event.Series[0].Name} at {@event.RacingEventLocation.Name}",
                $"{@event.Description}\n" +
                $"Starting {@event.StartDate:dd/MM/yyy}, ending {@event.EndDate:dd/MM/yyy}");
        }
    }
}
