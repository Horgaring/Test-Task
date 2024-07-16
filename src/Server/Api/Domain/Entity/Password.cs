using Api.DTO;

namespace Domain.Entity;

public class Password
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime CreatedAt { get; set; }

    public PasswordType Type { get; set; }

    internal void Update(PasswordUpdateRequest request)
    {
        Name = request.Name;
        Value = request.Value;
        Type = request.Type;
    }
}
