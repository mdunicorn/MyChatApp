using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazored.LocalStorage;
using MyChatApp.Client.Services;
using MyChatApp.Client.Services.ServiceImplementations;

namespace MyChatApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient(HttpClientHelper.AuthenticatedClientName, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient(HttpClientHelper.UnauthenticatedClientName, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddApiAuthorization();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<IUserIdentificationService, UserIdentificationService>();
            builder.Services.AddScoped<IUserHttpClientFactory, UserHttpClientFactory>();

            await builder.Build().RunAsync();
        }
    }
}
