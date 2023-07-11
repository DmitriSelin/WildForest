using ErrorOr;
using WildForest.Application.Authentication.Common;
using WildForest.Domain.Clients.Admins.Entites;
using WildForest.Domain.Clients.Users.Entities;

namespace WildForest.Application.Authentication.Queries.LoginUser;

public interface ILoginService
{
    public Task<ErrorOr<AuthenticationResult<User>>> LoginUserAsync(LoginQuery query);

    public Task<ErrorOr<AuthenticationResult<Admin>>> LoginAdminAsync(LoginQuery query);
}