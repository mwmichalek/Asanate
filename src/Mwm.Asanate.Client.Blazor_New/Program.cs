Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTA5OTE4QDMxMzkyZTMzMmUzMGZ4QnFHbGNEVFI4ZUcwYzZnVUJnQkFEcmxKUlFmQWl4RDc4VDdKcFRmcE09");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var services = builder.Services;



var myCloneApiUrl = builder.Configuration["MyClone:Api:Url"];
//var connectionString = builder.Configuration["ConnectionStrings:DatabaseContext"];
Console.WriteLine($"Middle Tier: [{myCloneApiUrl}]");
//Console.WriteLine($"Database: [{connectionString}]");

services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var configuration = services.AddConfigurationWithUserSecrets();
services.AddLogging();
services.AddClientServices();

builder.Services.AddSyncfusionBlazor();







await builder.Build().RunAsync();







