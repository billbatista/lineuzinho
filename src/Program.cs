namespace chimibot;

using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.MotorsportCalendar;

class Program
{
    static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

    private async Task MainAsync()
    {
        await using (var services = ConfigureServices())
        {
            var client = services.GetRequiredService<DiscordSocketClient>();

            client.Log += LogAsync;
            services.GetRequiredService<CommandService>().Log += LogAsync;

            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordBotToken") ?? string.Empty);
            await client.StartAsync();

            // Here we initialize the logic required to register our commands.
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

            await Task.Delay(Timeout.Infinite);
        }
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());

        return Task.CompletedTask;
    }

    private ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandService>()
            .AddSingleton<MotorsportCalendarService>()
            .AddSingleton<CommandHandlingService>()
            .AddHttpClient()
            .BuildServiceProvider();
    }
}
