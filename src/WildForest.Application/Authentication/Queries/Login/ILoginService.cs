using ErrorOr;
using WildForest.Application.Authentication.Common;

namespace WildForest.Application.Authentication.Queries.LoginUser;

public interface ILoginService
{
    public Task<ErrorOr<AuthenticationResult>> LoginAsync(LoginQuery query);
}