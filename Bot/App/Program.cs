using DrVegapunk.Bot.Web.Handlers;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace DrVegapunk.Bot.App;

public class Program {
    public async static Task Main(string[] args) {
        await WebHostManager.CreateBuilder(args)
                            .Build()
                            .RunAsync();

        // Wait infinitely so the bot actually stays connected.
        await Task.Delay(Timeout.Infinite);
    }
}
