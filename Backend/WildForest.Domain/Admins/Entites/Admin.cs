using WildForest.Domain.Admins.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Admins.Entites;

public class Admin : Person<AdminId>
{
    private Admin(
        AdminId id, FirstName firstName, LastName lastName,
        Role role, Email email, Password password) : base(id, firstName, lastName,
                                                            role, email, password)
    {
    }

    public static Admin Create(
        FirstName firstName, LastName lastName,
        Email email, Password password)
    {
        return new(AdminId.Create(), firstName,
                    lastName, Role.Admin,
                    email, password);
    }
}