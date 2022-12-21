namespace WildForest.Console.Services.ConsoleServices
{
    using System;
    using WildForest.Console.Cities.Services;
    using WildForest.Domain.Cities.Entities;

    public class GreatingService : IGreatingService
    {
        private readonly ICityService _cityService;

        public GreatingService(ICityService cityService)
        {
            _cityService = cityService;
        }

        public void StartDialog()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Welcome to WildForest console application!");
            Console.WriteLine("\n Please, enter file's name of someName.json \n");

            Console.ForegroundColor = ConsoleColor.Blue;
            string? fileName = Input();
            Console.ForegroundColor = ConsoleColor.Green;

            List<City> cities = _cityService.GetCitiesFromJsonFileAsync(fileName).Result;
            _cityService.AddCitiesAsync(cities);
        }

        private string? Input()
        {
            Console.Write(" ");
            return Console.ReadLine();
        }
    }
}