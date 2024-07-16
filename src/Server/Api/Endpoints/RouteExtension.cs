using Api.DTO;
using Api.Services;
using Domain;
using Domain.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class RouteExtension
{
    public static void MapEnpoints(this WebApplication app)
    {
        var api = app.MapGroup("/api/")
            .DisableAntiforgery()
            .WithTags("Passwords");
        api.MapGet("passwords", ([FromQuery] PasswordType? type,
            [FromQuery] string? search,
            [FromServices] PasswordService service) => 
        {
            var res = service.GetAll(search, type);
            return Results.Ok(res);
        });
        api.MapGet("passwords/{id}", (Guid id,
            [FromServices] PasswordService service) =>
        {
            var password = service.GetById(id);
            if(password == null) 
            {
                return Results.NotFound();
            }
            return Results.Ok(password);
        });
        api.MapPost("passwords", ([FromForm] PasswordRequest password,
            [FromServices] PasswordService service) =>
        {
            return service.Create(password);
        });
        api.MapDelete("passwords", ([FromQuery]Guid id,
            [FromServices] PasswordService service) =>
        {
            return service.Delete(id);
        });
        api.MapPut("passwords", ([FromForm] PasswordUpdateRequest request,
            [FromServices] PasswordService service) =>
        {
            return service.Update(request);
        });
    }

    
}