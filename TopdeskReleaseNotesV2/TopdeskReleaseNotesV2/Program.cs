using Radzen;
using Serilog;
using TopdeskReleaseNotesV2.Components;

var builder = WebApplication.CreateBuilder(args);

// Configure logging to write to a file
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders(); // Clear default logging providers (console, etc.)
    logging.AddSerilog(new LoggerConfiguration()
        .WriteTo.File("C:\\Windows\\Temp\\TDRN.txt")
        .CreateLogger());
});

try { 

    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents()
        .AddInteractiveWebAssemblyComponents();

    // Add HTTP client to the container.
    builder.Services.AddHttpClient();

    // Radzen
    builder.Services.AddRadzenComponents();

    // API Controllers
    builder.Services.AddControllers();

    // RadzenCookieThemeService
    builder.Services.AddRadzenCookieThemeService(options =>
    {
        options.Name = "MyApplicationTheme"; // The name of the cookie
        options.Duration = TimeSpan.FromDays(365); // The duration of the cookie
    });

    builder.Services.AddSingleton<HttpClient>(sp =>
    {
        var handler = new HttpClientHandler()
        {
            // SSL-validatie wordt uitgeschakeld (alle certificaten worden geaccepteerd)
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        return new HttpClient(handler);
    });;

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(TopdeskReleaseNotesV2.Client._Imports).Assembly);

    app.MapControllers();

    app.Logger.LogInformation("Application started!");
    app.Run();

}
catch (Exception ex)
{
    
    Log.Fatal(ex, "An unhandled exception occurred during application startup.");
    throw new InvalidOperationException("Something went wrong!");

}
finally
{
    Log.CloseAndFlush();
}