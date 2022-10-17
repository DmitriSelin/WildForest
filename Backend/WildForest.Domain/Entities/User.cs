using WildForest.Domain.Common.Enums;

namespace WildForest.Domain.Entities
{
    public record User(
        Guid Id, 
        string FirstName,
        string LastName,
        Role Role, 
        string Email,
        string Password);
}