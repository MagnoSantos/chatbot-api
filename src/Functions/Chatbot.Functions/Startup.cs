using Chatbot.Functions.ExternalServices;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Chatbot.Functions.Startup))]

namespace Chatbot.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.Configure<WeatherOptions>(opt =>
            {
                opt.ApiKey = Environment.GetEnvironmentVariable("ApiKeyWeather");
                opt.UrlBase = Environment.GetEnvironmentVariable("UrlBase");
            });
            builder.Services.AddTransient<IWeatherAgent, WeatherAgent>();
        }
    }
}