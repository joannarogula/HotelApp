using HotelBookingApp.Domain.Interfaces;
using HotelBookingApp.Domain.Services;
using HotelBookingApp.Infrastructure.Data;
using Microsoft.OpenApi.Models;

namespace WebApplication1;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        Console.WriteLine("\n WORKS!!!!! \n");

        services.AddMvc()
            .AddControllersAsServices(); 
        
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });
        
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Inicjalizacja Swagger UI
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

        // Kontynuacja konfiguracji Middleware
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Mapowanie kontrolerów
        });

    }
}