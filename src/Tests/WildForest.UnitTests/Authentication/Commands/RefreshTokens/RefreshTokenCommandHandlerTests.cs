using ErrorOr;
using NSubstitute;
using Shouldly;
using WildForest.Application.Authentication.Commands.RefreshTokens;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Infrastructure.Authentication;
using WildForest.UnitTests.Common;
using WildForest.UnitTests.TestUtils;

namespace WildForest.UnitTests.Authentication.Commands.RefreshTokens;

public sealed class RefreshTokenCommandHandlerTests : DbUnitTest
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public RefreshTokenCommandHandlerTests()
    {
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _refreshTokenGenerator = new RefreshTokenGenerator(UnitOfWork);
    }

    [Fact]
    public async Task RefreshTokenAsync_WithValidData_AuthResultReturned()
    {
        //Arrange
        var handler = new RefreshTokenCommandHandler(UnitOfWork, _refreshTokenGenerator, _jwtTokenGenerator);
        var refreshToken = UnitOfWork.Context.RefreshTokens.First();
        var command = new RefreshTokenCommand(refreshToken.Token, Constants.User.IP);
        
        //Act
        var authResult = await handler.RefreshTokenAsync(command);

        //Assert
        Assert.False(authResult.IsError);
    }

    [Fact]
    public async Task RefreshTokenAsync_WithNotExistingRefreshToken_ErrorReturned()
    {
        //Arrange
        var handler = new RefreshTokenCommandHandler(UnitOfWork, _refreshTokenGenerator, _jwtTokenGenerator);
        var command = new RefreshTokenCommand("Not exists token", Constants.User.IP);

        //Act
        var authResult = await handler.RefreshTokenAsync(command);

        //Assert
        Assert.True(authResult.IsError);
        authResult.FirstError.Type.ShouldBe(ErrorType.NotFound);
    }
}