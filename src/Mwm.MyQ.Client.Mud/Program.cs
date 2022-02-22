using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Mwm.MyQ.Client.Mud;
using MudBlazor.Services;
using Mwm.MyQ.Client.Service.Utils;
using Mwm.MyQ.Common.Utils;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//TEMPLATE: builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var services = builder.Services;
var myCloneApiUrl = builder.Configuration["MyClone:Api:Url"];
Console.WriteLine($"Middle Tier: [{myCloneApiUrl}]");

services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });
//OLD: services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });

var configuration = services.AddConfigurationWithUserSecrets();
services.AddLogging();
services.AddClientServices();

services.AddMudServices();

var server = builder.Build();
var serviceProvider = server.Services;

var logger = serviceProvider.GetService<ILogger<Program>>();
logger?.LogInformation(">>> STARTING SERVICE <<<");
await serviceProvider.UseClientServicesAsync();
logger?.LogInformation(">>> LOADED DATA <<<");

await server.RunAsync();
