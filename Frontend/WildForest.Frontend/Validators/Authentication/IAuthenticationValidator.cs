using WildForest.Frontend.Validators.Result;

namespace WildForest.Frontend.Validators.Authentication;

/// <summary>
/// Validator for user authentication
/// </summary>
internal interface IAuthenticationValidator
{
    /// <summary>
    /// Validate user registration
    /// </summary>
    /// <param name="firstName">First Name</param>
    /// <param name="lastName">Last Name</param>
    /// <param name="email">Email</param>
    /// <param name="password">Password</param>
    /// <param name="repeatedPassword">Equals password</param>
    /// <returns>ValidationResult</returns>
    ValidationResult<string> Validate(
        string firstName,
        string lastName,
        string email,
        string password,
        string repeatedPassword);

    /// <summary>
    /// Validate user login
    /// </summary>
    /// <param name="email">Email</param>
    /// <param name="password">Password</param>
    /// <returns>ValidationResult</returns>
    ValidationResult<string> Validate(string email, string password);
}