namespace WildForest.Dto.Models;

public abstract class UserDto : BaseDto
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }
    
    public string Password { get; init; }

    protected UserDto(Guid id, string firstName, string lastName, string email, string password) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }
}