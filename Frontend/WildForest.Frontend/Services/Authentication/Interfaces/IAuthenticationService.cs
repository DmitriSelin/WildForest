using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Authentication;

namespace WildForest.Frontend.Services.Authentication.Interfaces
{
    internal interface IAuthenticationService
    {
        Task<ResponseBase> RegisterAsync(RegisterRequest request);

        Task<ResponseBase> LoginAsync(LoginRequest request);
    }
}