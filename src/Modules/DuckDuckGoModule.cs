using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace chimibot.Modules;

public class DuckDuckGoModule : ModuleBase<SocketCommandContext>
{
    private readonly HttpClient httpClient;

    public DuckDuckGoModule(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = new Uri("https://api.duckduckgo.com/");
    }

    [Command("ddg")]
    [Alias("duckduckgo")]
    public async Task GetDuckDuckGoResult([Remainder] string query)
    {
        var response = await httpClient.GetFromJsonAsync<DuckDuckGoTopicResponse>(
            $"?q={query}&format=json&pretty=1");
        if (response?.Abstract == string.Empty)
        {
            var button = new ComponentBuilder()
                .WithButton(
                    "Go to duckduckgo",
                    null,
                    ButtonStyle.Link,
                    null,
                    $"https://www.duckduckgo.com/?q={query.Replace(" ", "+")}");

            await ReplyAsync(
                "Couldn't find any instant answer for this, so here's a duckduckgo search query:",
                components: button.Build());
            return;
        }

        var answer = new EmbedBuilder
        {
            Title = response?.Heading,
            Description = response?.AbstractText,
            Fields = new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder
                {
                    Name = "Url",
                    Value = response?.AbstractUrl
                }
            },
            Footer = new EmbedFooterBuilder
            {
                Text = "Results from duckduckgo.com",
                IconUrl = "https://pbs.twimg.com/profile_images/1452668733533601802/uSn3mxSe_400x400.jpg"
            }
        };
        await  ReplyAsync(embed: answer.Build());
    }
}

public record DuckDuckGoTopicResponse
{
    [JsonPropertyName("Heading")]
    public string Heading { get; set; }

    [JsonPropertyName("Abstract")]
    public string Abstract { get; set; }

    [JsonPropertyName("AbstractText")]
    public string AbstractText { get; set; }

    [JsonPropertyName("AbstractURL")]
    public string AbstractUrl { get; set; }
}
