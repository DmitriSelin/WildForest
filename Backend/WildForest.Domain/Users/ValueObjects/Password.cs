using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Users.ValueObjects;

public sealed class Password : ValueObject
{
    private const int keySize = 64;
    private const int iterations = 350_000;
    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA512;

    public string Value { get; private set; }

    private readonly byte[] _salt;

    private Password(string value)
    {
        _salt = RandomNumberGenerator.GetBytes(keySize);
        Value = HashPassword(value);
    }

    public static Password Create(string value)
    {
        string password = Validate(value);

        return new(password);
    }

    public bool IsEqual(string password)
    {
        password = Validate(password);
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, _salt, iterations, _hashAlgorithmName, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromBase64String(Value));
    }

    public override string ToString()
        => Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private string HashPassword(string password)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password: Encoding.UTF8.GetBytes(password),
            salt: _salt,
            iterations: iterations,
            hashAlgorithm: _hashAlgorithmName,
            outputLength: keySize);

        return Convert.ToBase64String(hash);
    }

    private static string Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        var password = value.Trim();

        if (password.Length < 6 || password.Length > 50)
            throw new ValidationException("Invalid Password");

        return password;
    }
}