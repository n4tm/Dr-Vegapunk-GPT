using System.Diagnostics;
using System.Reflection;

namespace DrVegapunk.Web;

public class Program {
    public static Task Main(string[] args) => MainAsync(args);

    private static async Task MainAsync(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        await app.RunAsync();
    }

    // TODO: Find a best approach
    public static void Run() {
        var dllPath = Assembly.GetExecutingAssembly().Location;
        Process.Start(dllPath[..^4] + ".exe");
    }
}