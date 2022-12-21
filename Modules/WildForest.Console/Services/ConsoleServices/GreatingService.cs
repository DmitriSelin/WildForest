namespace WildForest.Console.Services.ConsoleServices
{
    using System;

    public class GreatingService : IGreatingService
    {
        public void StartDialog()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Welcome to WildForest console application!");
            Console.WriteLine("\n Please, enter file's name of someName.json \n");

            Console.ForegroundColor = ConsoleColor.Blue;
            string? fileName = Enter();
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private string? Enter()
        {
            Console.Write(" ");
            return Console.ReadLine();
        }
    }
}