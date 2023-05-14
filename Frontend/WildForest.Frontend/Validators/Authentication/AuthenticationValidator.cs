using System.ComponentModel.DataAnnotations;
using WildForest.Frontend.Validators.Result;

namespace WildForest.Frontend.Validators.Authentication
{
    internal class AuthenticationValidator : IAuthenticationValidator
    {
        public ValidationResult<string> Validate(string firstName, string lastName, string email, string password, string repeatedPassword)
        {
            var isFirstNameValid = ValidateFirstName(firstName);

            if (!isFirstNameValid.isValid)
                return isFirstNameValid;

            var isLastNameValid = ValidateLastName(lastName);

            if (!isLastNameValid.isValid)
                return isLastNameValid;

            var isEmailValid = ValidateEmail(email);

            if (!isEmailValid.isValid)
                return isEmailValid;

            var isPasswordValid = ValidatePassword(password);

            if (!isPasswordValid.isValid)
                return isPasswordValid;

            var isRepeatedPasswordValid = ValidatePasswords(password, repeatedPassword);

            if (!isRepeatedPasswordValid.isValid)
                return isRepeatedPasswordValid;

            return new("The data are valid", true, null);
        }

        public ValidationResult<string> Validate(string email, string password)
        {
            var isEmailValid = ValidateEmail(email);

            if (!isEmailValid.isValid)
                return isEmailValid;

            var isPasswordValid = ValidatePassword(password);

            if (!isPasswordValid.isValid)
                return isPasswordValid;

            return new("The data are valid", true, null);
        }

        private ValidationResult<string> ValidateFirstName(string firstName)
        {
            return ValidateName(firstName, "first name");
        }

        private ValidationResult<string> ValidateLastName(string lastName)
        {
            return ValidateName(lastName, "last name");
        }

        private ValidationResult<string> ValidateEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new(value, false, "Enter email");

            string email = value.Trim();

            var isValid = new EmailAddressAttribute().IsValid(email);

            if (isValid)
            {
                return new(email, true, null);
            }

            return new(email, false, "Enter correct email");
        }

        private ValidationResult<string> ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return new(password, false, "Enter password");

            if (password.Length is < 6 or > 50)
                return new(password, false, "Password must contain from 6 to 50 letters");

            return new(password, true, null);
        }

        private ValidationResult<string> ValidatePasswords(string password, string repeatedPassword)
        {
            if (password != repeatedPassword)
                return new(repeatedPassword, false, "Enter the same passords");

            return new(password, true, null);
        }

        private ValidationResult<string> ValidateName(string value, string title)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new(value, false, $"Enter {title}");

            var name = value.Trim();

            if (name.Length is < 2 or > 50)
                return new(value, false, $"{title} must contain from 2 to 50 letters");

            return new(value, true, null);
        }
    }
}