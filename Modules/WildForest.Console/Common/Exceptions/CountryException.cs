namespace WildForest.Console.Common.Exceptions
{
    internal class CountryException : Exception
    {
        internal CountryException() { }

        internal CountryException(string messageForUser)
        {
            MessageForUser = messageForUser;
        }

        internal string MessageForUser { get; } = null!;
    }
}