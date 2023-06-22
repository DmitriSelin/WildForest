using WildForest.Frontend.Validators.Authentication;

namespace WildForest.UnitTests.Validators;

[TestClass]
internal class AuthenticationValidatorTests
{
    [TestMethod]
    public void Validate_WithValidData_ReturnedSuccess()
    {
        //Arrange

        var firstName = "Dmitri";
        var lastName = "Selin";
        var email = "selin@gmail.com";
        var password = "123456";
        var samePassword = "123456";

        //Act

        //var authValidator = new AuthenticationValidator();
        var dto = new CityDto();

        //Assert
    }
}