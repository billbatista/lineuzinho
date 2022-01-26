# Discord Bot

A very quick and dirty Discord bot just to mess around.
To build/compile/publish you can use Nuke. Go to the project folder and run `dotnet tool restore`, after that you can use Nuke with `dotnet nuke <target>` where target is either clean/restore/compile/publish.

Or just use the dotnet commands, I used Nuke on this just to see how it works.

Don't forget to set your bot token as an environment variable at the `launchSettings.json` file.

## To do
* move the calendar.json file to some external object storage
* add github actions for automatic deploys (maybe self-hosted)
* keep adding stupid commands
