namespace WildForest.Application.Common.Exceptions
{
    public class UserException : Exception
    {
        public string? MessageForUser { get; }

        public int? StatusCode { get; init; }

        public UserException() { }

        public UserException(string message) : base(message) { }

        public UserException(string message, Exception innerException) : base(message, innerException) { }

        public UserException(string message, string messageForUser, int? statusCode) : base(message)
        {
            MessageForUser = messageForUser;
            StatusCode = statusCode;
        }
    }
}