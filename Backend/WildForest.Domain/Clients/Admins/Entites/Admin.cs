using WildForest.Domain.Clients.Models;
using WildForest.Domain.Clients.ValueObjects;
using WildForest.Domain.Common.Enums;

namespace WildForest.Domain.Clients.Admins.Entites;

public class Admin : Person
{
    private Admin(
        PersonId id, FirstName firstName, LastName lastName,
        Role role, Email email, Password password) : base(id, firstName, lastName,
                                                            role, email, password)
    {
    }

    public static Admin Create(
        FirstName firstName, LastName lastName,
        Email email, Password password)
    {
        return new(PersonId.Create(), firstName,
                    lastName, Role.Admin,
                    email, password);
    }
}