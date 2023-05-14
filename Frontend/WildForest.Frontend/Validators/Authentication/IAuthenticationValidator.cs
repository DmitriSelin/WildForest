using WildForest.Frontend.Validators.Result;

namespace WildForest.Frontend.Validators.Authentication
{
    internal interface IAuthenticationValidator
    {
        ValidationResult<string> Validate(
            string firstName,
            string lastName,
            string email,
            string password,
            string repeatedPassword);

        ValidationResult<string> Validate(string email, string password);
    }
}