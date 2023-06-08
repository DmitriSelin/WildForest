using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Authentication;

namespace WildForest.Frontend.Services.Authentication.Interfaces
{
    /// <summary>
    /// Service for register and login
    /// </summary>
    internal interface IAuthenticationService
    {
        /// <summary>
        /// Method for registration in app by new user
        /// </summary>
        /// <param name="request">RegisterRequest</param>
        /// <returns>ResponseBase</returns>
        Task<ResponseBase> RegisterAsync(RegisterRequest request);

        /// <summary>
        /// Method for login in app
        /// </summary>
        /// <param name="request">LoginRequest</param>
        /// <returns>ResponseBase</returns>
        Task<ResponseBase> LoginAsync(LoginRequest request);
    }
}