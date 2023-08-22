using Blazored.LocalStorage;
using BlazorSozluk.WebApp;
using BlazorSozluk.WebApp.Infastructure.Services;
using BlazorSozluk.WebApp.Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WebApiClient",client =>
{
    client.BaseAddress = new Uri(builder.Configuration["https://localhost:5001"]);
});

builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient("WebApiClient");
});

builder.Services.AddTransient<IVoteServices, VoteServices>();
builder.Services.AddTransient<IEntryService,EntryService>();
builder.Services.AddTransient<IFavServices,FavServices>();
builder.Services.AddTransient<IIdentityService,IdentityService>();
builder.Services.AddTransient<IUserService,UserService>();


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
