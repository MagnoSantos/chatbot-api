using Chatbot.API.HostedServices;
using Chatbot.API.Options;
using Chatbot.Domain.Implementations;
using Chatbot.Domain.Interfaces;
using Chatbot.Infraestructure.ExternalServices;
using Chatbot.Infraestructure.ExternalServices.Options;
using Chatbot.Infraestructure.MessageBroker;
using Chatbot.Infraestructure.MessageBroker.Options;
using Chatbot.Infrastructure.ExternalServices.Facebook;
using Chatbot.Infrastructure.ExternalServices.Facebook.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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

            //Configurar options da aplicação
            services.Configure<WebhookOptions>(opt =>
            {
                opt.AppSecret = Configuration.GetValue<string>("AppConfiguration:AppSecret");
                opt.VerifyToken = Configuration.GetValue<string>("AppConfiguration:VerifyToken");
            });
            services.Configure<ServiceBusOptions>(opt =>
            {
                opt.ConnectionString = Configuration.GetConnectionString("ServiceBusConnectionString");
            });
            services.Configure<WatsonAssistantOptions>(opt =>
            {
                opt.ApiKey = Configuration.GetValue<string>("AppConfiguration:AppSecret");
                opt.UrlAuth = Configuration.GetValue<string>("AppConfiguration:UrlAuth");
            });
            services.Configure<FacebookOptions>(opt =>
            {
                opt.PageAcessToken = Configuration.GetValue<string>("AppConfiguration:VerifyToken");
                opt.UrlBase = Configuration.GetValue<string>("AppConfiguration:UrlBaseFacebook");
                opt.VersaoApi = Configuration.GetValue<string>("AppConfiguration:VersaoApiFacebook");
                opt.TimeOut = TimeSpan.FromSeconds(10);
            });

            //Configurar dependências da aplicação
            services.AddTransient<IWebhookHandler, WebhookHandler>();
            services.AddTransient<IClientMessageBroker, ServiceBusMessageBroker>();
            services.AddTransient<IWatsonAssistantAuth, WatsonAssistantAuth>();
            services.AddTransient<IWatsonAssistant, WatsonAssistant>();
            services.AddTransient<IMessageProcessHandler, MessageProcessHandler>();
            services.AddTransient<IFacebookAgent, FacebookAgent>();

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