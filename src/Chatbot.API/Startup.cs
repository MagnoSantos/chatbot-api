using Chatbot.API.HostedServices;
using Chatbot.API.Options;
using Chatbot.Domain.Implementations;
using Chatbot.Domain.Interfaces;
using Chatbot.Infraestructure.MessageBroker;
using Chatbot.Infraestructure.MessageBroker.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            //Configurar dependências da aplicação
            services.AddTransient<IWebhookHandler, WebhookHandler>();
            services.AddTransient<IClientMessageBroker, ServiceBusMessageBroker>();

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
