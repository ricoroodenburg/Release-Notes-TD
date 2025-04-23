using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Radzen
builder.Services.AddRadzenComponents();


await builder.Build().RunAsync();