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

            int choice = SelectAction();

            if (choice == 1)
            {

            }
            else if (choice == 2)
            {
                Console.WriteLine("\n Please, enter file's name of someName.json \n");

                string? fileName = Input();

                if (fileName is null)
                {
                    throw new ArgumentNullException(nameof(fileName));
                }

                List<City> cities = _cityService.GetCitiesFromJsonFileAsync(fileName).Result;
                _cityService.AddCitiesAsync(cities);
            }
        }

        private string? Input()
        {
            Console.ForegroundColor = ConsoleColor.Blue;            
            Console.Write(" ");
            string? input = Console.ReadLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;

            return input;
        }

        private int SelectAction()
        {
            Console.WriteLine("\n If you want to add country, enter 1;\n\n" +
                " For downloading cities from file, enter 2\n");

            int input;

            do
            {
                Console.WriteLine(" Enter 1 or 2\n");
                Int32.TryParse(Input(), out input);

                if (input == 1 || input == 2)
                {
                    break;
                }
            }
            while(true);

            return input;
        }
    }
}