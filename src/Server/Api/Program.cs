using System.Text.Json.Serialization;
using Api.Endpoints;
using Domain.Entity;
using Microsoft.OpenApi.Models;
using Serilog;
using FluentValidation;
using Api.DTO;
using Api.Validations;
using Api.Services;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        try{
            Log.Information("Starting web host");
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Host.UseSerilog((cbx,lc) =>lc
                .WriteTo.Console()
            );
            builder.Services.ConfigureHttpJsonOptions(p => 
            {
                p.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddSingleton<List<Password>>();
            builder.Services.AddScoped<PasswordService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(p => {
                p.AddDefaultPolicy(p => p
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            });
            builder.Services.AddScoped<IValidator<Password>, PasswordValidation>();
            builder.Services.AddSwaggerGen(p =>
            {
                p.SwaggerDoc("v1", new OpenApiInfo() { Version = "v1", Title = "Password" });
            });
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    
                });
            }
            app.UseRouting();
            app.UseCors();
            app.MapEnpoints();
            app.Run();
        }
        catch (Exception ex){
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally{
            Log.CloseAndFlush();
        }
    }
}