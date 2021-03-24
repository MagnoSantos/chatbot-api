using Chatbot.API.HostedServices;
using Chatbot.Domain.Implementations;
using Chatbot.Domain.Implementations.Actions;
using Chatbot.Domain.Interfaces;
using Chatbot.Infraestructure.ExternalServices;
using Chatbot.Infraestructure.ExternalServices.Options;
using Chatbot.Infraestructure.MessageBroker;
using Chatbot.Infraestructure.MessageBroker.Options;
using Chatbot.Infrastructure.ExternalServices.HG_Weater;
using Chatbot.Infrastructure.ExternalServices.HG_Weater.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatbotAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<ServiceBusOptions>(opt =>
            {
                opt.ConnectionString = Configuration.GetConnectionString("ServiceBusConnectionString");
            });
            services.Configure<WatsonAssistantOptions>(opt =>
            {
                opt.ApiKey = Configuration.GetValue<string>("AppConfiguration:AppSecret");
                opt.UrlAuth = Configuration.GetValue<string>("AppConfiguration:UrlAuth");
                opt.UrlWatson = Configuration.GetValue<string>("AppConfiguration:UrlBaseWatson");
                opt.AssistantId = Configuration.GetValue<string>("AppConfiguration:AssistantId");
                opt.Version = Configuration.GetValue<string>("AppConfiguration:Version");
            });
            services.Configure<HGWeatherOptions>(opt =>
            {
                opt.ApiKey = Configuration.GetValue<string>("AppConfiguration:ApiKeyWeather");
                opt.UrlBase = Configuration.GetValue<string>("AppConfiguration:ApiKeyWeather");
            });

            //Configurar dependências da aplicação
            services.AddTransient<IWebhookHandler, WebhookHandler>();
            services.AddTransient<IClientMessageBroker, ServiceBusMessageBroker>();
            services.AddTransient<IWatsonAssistantAuth, WatsonAssistantAuth>();
            services.AddTransient<IWatsonAssistant, WatsonAssistant>();
            services.AddTransient<IActionHandler, ActionHandler>();
            services.AddTransient<IActionFactory, ActionFactory>();
            services.AddTransient<IMessageProcessHandler, MessageProcessHandler>();
            services.AddTransient<IAction, ActionTemperatura>();
            services.AddTransient<IHGWeather, HGWeather>();

            //Hosted Services
            services.AddHostedService<MessageProcessHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}