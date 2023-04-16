using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DrVegapunk.Bot.App.Utils;

public static class LogManager {
    public static void RegisterLog(CommandService commandSvc) =>
        commandSvc.Log += Log;

    public static void RegisterLog(DiscordSocketClient client) =>
        client.Log += Log;

    public static Task Log(LogMessage message) {
        switch (message.Severity) {
            case LogSeverity.Critical:
            case LogSeverity.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case LogSeverity.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case LogSeverity.Info:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case LogSeverity.Verbose:
            case LogSeverity.Debug:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
        }
        Console.WriteLine($"{DateTime.Now,-19} [{message.Severity}] {message.Source}: {message.Message} {message.Exception}");
        Console.ResetColor();

        return Task.CompletedTask;
    }
}
