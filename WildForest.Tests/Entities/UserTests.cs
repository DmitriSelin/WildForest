using System.ComponentModel.DataAnnotations;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Tests.Entities;

[TestClass]
public sealed class UserTests
{
    [TestMethod]
    public void CreateUser_WithValidData_ReturnesUser()
    {
        //Arrange
        var firstName = FirstName.Create("Dmitri");
        var lastName = LastName.Create("Voliev");
        var email = Email.Create("val@gmail.com");
        var password = Password.Create("hjbdgshu78");
        var cityId = CityId.Create();

        //Act
        var user = User.Create(firstName, lastName, email, password, cityId);

        //Assert
        Assert.IsNotNull(user);
    }

    [TestMethod]
    public void CreateEmail_WithInvalidEmail_ExceptionThrows()
    {
        //Arrange
        Email email;

        try
        {
            //Act
            email = Email.Create("var@@gmail.com");

            //Assert
            Assert.Fail();
        }
        catch (ValidationException)
        {
            //Assert
            Assert.IsTrue(true);
        }
    }

    [TestMethod]
    public void CreatePassword_WithInvalidData_ExceptionThrows()
    {
        //Arrange
        Password password;

        try
        {
            //Act
            password = Password.Create("12345");    //min - 6

            //Assert
            Assert.Fail();
        }
        catch (ValidationException)
        {
            //Assert
            Assert.IsTrue(true);
        }
    }

    [TestMethod]
    public void CreateName_WithInvalidData_ExceptionThrows()
    {
        //Arrange
        FirstName firstName;

        try
        {
            //Act
            firstName = FirstName.Create("U");

            //Assert
            Assert.Fail();
        }
        catch (ValidationException)
        {
            //Assert
            Assert.IsTrue(true);
        }
    }
}