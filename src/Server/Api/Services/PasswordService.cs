using Api.DTO;
using Api.Exceptions;
using Domain;
using Domain.Entity;
using FluentValidation;

namespace Api.Services;

public class PasswordService
{
    private readonly List<Password> passwords;
    private readonly IValidator<Password> validator;

    public PasswordService(List<Password> passwords, IValidator<Password> validator)
        => (this.passwords, this.validator) = (passwords, validator);

    
    public IResult Update(PasswordUpdateRequest request)
    {
        var entity = passwords.FirstOrDefault(p => p.Id == request.Id);
        passwords.Remove(entity);
        if (entity == null)
        {
            return Results.NotFound();
        }
        var result = validator.Validate(request.ToPassword());
        if (!result.IsValid)
        {
            passwords.Add(entity);
            return Results.BadRequest(CustomValidationException.Create(result.Errors));
        }
        if(passwords.Any(p => p.Name == request.Name))
        {
            passwords.Add(entity);
            return Results.BadRequest(CustomValidationException.Create([new FluentValidation.Results.ValidationFailure("Name", "Name already exist")]));
        }
        entity.Update(request);
        passwords.Add(entity);
        return Results.Created();
    }

    public IResult Delete(Guid id)
    {
        if (passwords.FirstOrDefault(p => p.Id == id) == null)
        {
            return Results.NotFound();
        }
        passwords.RemoveAll(p => p.Id == id);
        return Results.Ok();
    }

    public IResult Create(PasswordRequest request)
    {
        var entity = request.ToPassword();
        var result = validator.Validate(entity);
        if (!result.IsValid)
        {
            return Results.BadRequest(CustomValidationException.Create(result.Errors));
        }
        if(passwords.Any(p => p.Name == request.Name))
        {
            return Results.BadRequest(CustomValidationException.Create([new FluentValidation.Results.ValidationFailure("Name", "Name already exist")]));
        }
        passwords.Add(entity);
        return Results.Created($"/api/passwords/{entity.Id}", entity);
    }
    public List<Password> GetAll(string? search, PasswordType? type)
    {
        return passwords
                .Where(p => type == null || p.Type == type)
                .Where(p => string.IsNullOrEmpty(search) 
                    || p.Name.Contains(search))
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
    }
    public Password? GetById(Guid id)
    {
        return passwords
            .FirstOrDefault(p => p.Id == id);
    }
}
