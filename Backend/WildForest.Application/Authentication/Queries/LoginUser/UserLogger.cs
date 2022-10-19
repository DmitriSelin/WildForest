using WildForest.Application.Authentication.Common;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Entities;

namespace WildForest.Application.Authentication.Queries.LoginUser
{
    public class UserLogger : IUserLogger
    {
        private readonly IUserRepository _userRepository;

        public UserLogger(IUserRepository uesrRepository)
        {
            _userRepository = uesrRepository;
        }

        public AuthenticationResult Login(LoginUserQuery query)
        {
            User? user = _userRepository.GetUserByEmailAsync(query.Email).Result;

            if (user == null)
            {
                throw new Exception();
            }

            if (query.Password != user.Password)
            {
                throw new Exception();
            }

            return new AuthenticationResult(user, "token");
        }
    }
}