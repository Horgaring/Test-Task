using Domain;
using Domain.Entity;

namespace Api.DTO;

public class PasswordUpdateRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }

    public PasswordType Type { get; set; }

    internal Password ToPassword()
    {
        return new Password
        {
            Id = Id,
            Name = Name,
            Value = Value,
            Type = Type
        };
    }
}
