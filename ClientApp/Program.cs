using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;
using ClientApp.Services;

// Initialize the Blazor WebAssembly host builder
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register root components - App is the main component, HeadOutlet manages the <head> element
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient with the app's base address as a scoped service
// In production the BaseAddress would point to the deployed API URL rather than the host environment
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register ProductService for dependency injection across all components
builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
