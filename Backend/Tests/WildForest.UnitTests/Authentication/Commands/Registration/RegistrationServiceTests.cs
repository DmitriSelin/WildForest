using WildForest.UnitTests.TestUtils;
using NSubstitute;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.UnitTests.Common;
using WildForest.Application.Authentication.Commands.Registration;
using ErrorOr;
using WildForest.Application.Authentication.Common;
using Shouldly;
using WildForest.Infrastructure.Authentication;

namespace WildForest.UnitTests.Authentication.Commands.Registration;

public sealed class RegistrationServiceTests : DbUnitTest
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly Guid _languageId;

    public RegistrationServiceTests()
    {
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _refreshTokenGenerator = new RefreshTokenGenerator(UnitOfWork);
        _languageId = UnitOfWork.Context.Languages.First().Id.Value;
    }

    [Fact]
    public async Task RegisterAsync_WithValidData_AuthResultReturned()
    {
        //Arrange
        var registrationService = new RegistrationService(_jwtTokenGenerator, _refreshTokenGenerator, UnitOfWork);
        var city = UnitOfWork.Context.Cities.First();
        var command = new RegisterCommand(Constants.User.FirstName.Value, Constants.User.LastName.Value,
            Constants.User.FirstEmail.Value, Constants.User.Password.Value, Constants.User.IP, city.Id.Value, _languageId);

        //Act
        ErrorOr<AuthenticationResult> authResult = await registrationService.RegisterAsync(command);

        //Assert
        Assert.False(authResult.IsError);
    }

    [Fact]
    public async Task RegisterAsync_WithExistingUser_ErrorReturned()
    {
        //Arrange
        var registrationService = new RegistrationService(_jwtTokenGenerator, _refreshTokenGenerator, UnitOfWork);
        var city = UnitOfWork.Context.Cities.First();
        var command = new RegisterCommand(Constants.User.FirstName.Value, Constants.User.LastName.Value,
            Constants.User.UserDuplicateEmail.Value, Constants.User.Password.Value, Constants.User.IP, city.Id.Value, _languageId);

        //Act
        ErrorOr<AuthenticationResult> authResult = await registrationService.RegisterAsync(command);

        //Assert
        Assert.True(authResult.IsError);
        authResult.FirstError.Type.ShouldBe(ErrorType.Conflict);
    }

    [Fact]
    public async Task RegisterAsync_WithNotExistsCity_ErrorReturned()
    {
        //Arrange
        var registrationService = new RegistrationService(_jwtTokenGenerator, _refreshTokenGenerator, UnitOfWork);
        var command = new RegisterCommand(Constants.User.FirstName.Value, Constants.User.LastName.Value,
            Constants.User.FirstEmail.Value, Constants.User.Password.Value, Constants.User.IP, Guid.NewGuid(), _languageId);

        //Act
        ErrorOr<AuthenticationResult> authResult = await registrationService.RegisterAsync(command);

        //Assert
        Assert.True(authResult.IsError);
        authResult.FirstError.Type.ShouldBe(ErrorType.NotFound);
    }
}