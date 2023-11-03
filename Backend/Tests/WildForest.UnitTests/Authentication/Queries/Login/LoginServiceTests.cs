using ErrorOr;
using NSubstitute;
using Shouldly;
using System.ComponentModel.DataAnnotations;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Infrastructure.Authentication;
using WildForest.UnitTests.Common;
using WildForest.UnitTests.TestUtils;

namespace WildForest.UnitTests.Authentication.Queries.Login;

public class LoginServiceTests : DbUnitTest
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public LoginServiceTests()
    {
        _refreshTokenGenerator = new RefreshTokenGenerator(UnitOfWork);
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
    }

    [Fact]
    public async Task LoginAsync_WithValidData_AuthResultReturned()
    {
        //Arrange
        var loginService = new LoginService(_jwtTokenGenerator, _refreshTokenGenerator, UnitOfWork);
        var query = new LoginQuery(Constants.User.UserDuplicateEmail.Value, Constants.User.Password, Constants.User.IP);

        //Act
        ErrorOr<AuthenticationResult> authResult = await loginService.LoginAsync(query);

        //Assert
        Assert.False(authResult.IsError);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidEmail_ExceptionThrow()
    {
        //Arrange
        var loginService = new LoginService(_jwtTokenGenerator, _refreshTokenGenerator, UnitOfWork);
        var query = new LoginQuery("invalidEmail", Constants.User.Password, Constants.User.IP);

        //Act & Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await loginService.LoginAsync(query));
    }

    [Fact]
    public async Task LoginAsync_WithNotExistsUser_ErrorReturned()
    {
        //Arrange
        var loginService = new LoginService(_jwtTokenGenerator, _refreshTokenGenerator, UnitOfWork);
        var query = new LoginQuery("notexists@gmail.com", Constants.User.Password, Constants.User.IP);

        //Act
        ErrorOr<AuthenticationResult> authResult = await loginService.LoginAsync(query);

        //Assert
        Assert.True(authResult.IsError);
        authResult.FirstError.Type.ShouldBe(ErrorType.NotFound);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidPassword_ErrorReturned()
    {
        //Arrange
        var loginService = new LoginService(_jwtTokenGenerator, _refreshTokenGenerator, UnitOfWork);
        var query = new LoginQuery(Constants.User.UserDuplicateEmail.Value, "invalidPassword", Constants.User.IP);

        //Act
        ErrorOr<AuthenticationResult> authResult = await loginService.LoginAsync(query);

        //Assert
        Assert.True(authResult.IsError);
        authResult.FirstError.Type.ShouldBe(ErrorType.NotFound);
    }
}