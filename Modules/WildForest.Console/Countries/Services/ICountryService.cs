namespace WildForest.Console.Countries.Services
{
    public interface ICountryService
    {
        Task AddCountryAsync(string countryName);
    }
}
