using Chatbot.Domain.Implementations;
using Chatbot.Domain.Interfaces;
using Chatbot.Infraestructure.ExternalServices;
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

            services.Configure<HGWeatherOptions>(opt =>
            {
                opt.ApiKey = Configuration.GetValue<string>("AppConfiguration:ApiKeyWeather");
                opt.UrlBase = Configuration.GetValue<string>("AppConfiguration:UrlBase");
            });

            //Configurar dependências da aplicação
            services.AddTransient<IWebhookHandler, WebhookHandler>();
            services.AddTransient<IWeatherHandler, WeatherHandler>();
            services.AddTransient<IHGWeather, HGWeather>();

            //Configurar swagger
            services.AddSwaggerGen();
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MRV DIO API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}