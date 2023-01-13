namespace WildForest.Console.Services.ConsoleServices
{
    using System;
    using WildForest.Console.Cities.Services;
    using WildForest.Console.Countries.Services;
    using WildForest.Domain.Cities.Entities;

    public class GreatingService : IGreatingService
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public GreatingService(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }

        public async void StartDialog()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Welcome to WildForest console application!");

            while (true)
            {
                int choice = SelectAction();

                try
                {

                    if (choice == 1)
                    {
                        AddCountry();
                    }
                    else if (choice == 2)
                    {
                        AddCities();
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("\n Not correct name\n");
                    continue;
                }
            }
        }

        private static string? Input()
        {
            Console.ForegroundColor = ConsoleColor.Blue;            
            Console.Write(" ");
            string? input = Console.ReadLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;

            return input;
        }

        private void AddCountry()
        {
            Console.WriteLine("\n Please, enter country's name\n");

            string? countryName = Input();

            if (countryName == null || countryName == string.Empty)
            {
                throw new ArgumentNullException(nameof(countryName));
            }

            _countryService.AddCountryAsync(countryName);
        }

        private void AddCities()
        {
            Console.WriteLine("\n Please, enter file's name of someName.json \n");

            string? fileName = Input();

            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            Console.WriteLine("\n Please, enter existing country's name");

            string? countryName = Input();

            if (countryName is null)
            {
                throw new ArgumentNullException(nameof(countryName));
            }

            List<City> cities = _cityService.GetCitiesFromJsonFileAsync(fileName, countryName).Result;
            _cityService.AddCitiesAsync(cities);
        }

        private static int SelectAction()
        {
            Console.WriteLine("\n If you want to add country, enter 1;\n\n" +
                " For downloading cities from file, enter 2\n");

            int condition1 = 1;
            int condition2 = 2;

            int input;

            do
            {
                Console.WriteLine(" Enter 1 or 2\n");
                int.TryParse(Input(), out input);

                if (input == condition1 || input == condition2)
                {
                    break;
                }
            }
            while(true);

            return input;
        }
    }
}