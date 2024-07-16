using Domain;
using Domain.Entity;

namespace Api.DTO;

public class PasswordRequest
{
    public string Name { get; set; }
    public string Value { get; set; }

    public PasswordType Type { get; set; }

    public Password ToPassword()
    {
        return new Password
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Value = Value,
            CreatedAt = DateTime.Now,
            Type = Type
        };
    }
}
