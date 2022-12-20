StartDialog();

static void StartDialog()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n Welcome to WildForest console application!");
    Console.WriteLine("\n Please, enter file's name of someName.json \n");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(" ");
    string fileName = Console.Read().ToString();
    Console.ForegroundColor = ConsoleColor.Green; 
}