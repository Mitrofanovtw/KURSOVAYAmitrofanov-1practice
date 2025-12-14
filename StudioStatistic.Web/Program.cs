using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using StudioStatistic.Web;
using StudioStatistic.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7146") });
builder.Services.AddScoped<IApiService>(sp => RestService.For<IApiService>(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
