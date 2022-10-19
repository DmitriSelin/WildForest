using WildForest.Application.Authentication.Common;

namespace WildForest.Application.Authentication.Queries.LoginUser
{
    public interface IUserLogger
    {
        public AuthenticationResult Login(LoginUserQuery query);
    }
}