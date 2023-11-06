using Shouldly;
using WildForest.Application.Authentication.Queries.Registration;
using WildForest.UnitTests.Common;

namespace WildForest.UnitTests.Authentication.Queries.Registration;

public class AuthCredentialsQueryHandlerTests : DbUnitTest
{
    [Fact]
    public async Task GetCountriesAndLanguagesAsync_AuthCredentialsReturned()
    {
        //Arrange
        var authCredentialsQueryHandler = new AuthCredentialsQueryHandler(UnitOfWork);

        //Act
        AuthCredentials credentials = await authCredentialsQueryHandler.GetCountriesAndLanguagesAsync();

        //Assert
        Assert.NotNull(credentials);
        credentials.Languages.Count().ShouldBeGreaterThanOrEqualTo(1);
        credentials.Countries.Count().ShouldBeGreaterThanOrEqualTo(1);
    }
}