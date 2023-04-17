namespace DrVegapunk.Bot.Web;

public class Startup {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration config) {
        Configuration = config;
    }

    public void ConfigureServices(IServiceCollection services) {
        services.AddControllersWithViews();
        services.AddSingleton<HostedConsoleService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }

        // app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        app.ApplicationServices.GetService<HostedConsoleService>()!
           .StartAsync(new CancellationToken())
           .Wait();
    }
}
