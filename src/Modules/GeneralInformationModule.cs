using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace chimibot.Modules;

// Modules must be public and inherit from an IModuleBase
public class GeneralInformationModule : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Alias("pong", "hello")]
    public Task PingAsync() => ReplyAsync("pong!");

    // Get info on a user, or the user who invoked the command if one is not specified
    [Command("userinfo")]
    public async Task UserInfoAsync(IUser user = null)
    {
        user ??= Context.User;

        await ReplyAsync(user.ToString());
    }

    // Ban a user
    // [Command("ban")]
    // [RequireContext(ContextType.Guild)]
    // make sure the user invoking the command can ban
    // [RequireUserPermission(GuildPermission.BanMembers)]
    // make sure the bot itself can ban
    // [RequireBotPermission(GuildPermission.BanMembers)]
    // public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
    // {
    //     await user.Guild.AddBanAsync(user, reason: reason);
    //     await ReplyAsync("ok!");
    // }

    // [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
    // Insert a ZWSP before the text to prevent triggering other bots!
    [Command("echo")]
    public async Task EchoAsync([Remainder] string text) => await ReplyAsync('\u200B' + text);

    // 'params' will parse space-separated elements into a list
    [Command("list")]
    public async Task ListAsync(params string[] objects)
        => await ReplyAsync("You listed: " + string.Join("; ", objects));

    // Setting a custom ErrorMessage property will help clarify the precondition error
    [Command("guild_only")]
    [RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not a DM!")]
    public async Task GuildOnlyCommand() => await ReplyAsync("Nothing to see here!");
}
