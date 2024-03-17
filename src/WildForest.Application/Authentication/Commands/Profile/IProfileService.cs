using ErrorOr;
using WildForest.Application.Authentication.Common;

namespace WildForest.Application.Authentication.Commands.Profile;

public interface IProfileService
{
    Task<ErrorOr<AuthenticationResult>> UpdateAsync(UpdateProfileCommand command);
}