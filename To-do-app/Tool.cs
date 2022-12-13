namespace ToDoApp
{
    public static class Tool
    {
        public static bool Confirm()
        {
            while (true)
            {
                Console.WriteLine("Confirm? y/n");
                var input = Console.ReadLine()
                    .ToLower()
                    .Trim();

                if (input == "y" || input == "yes")
                    return true;
                else if (input == "n" || input == "no")
                    return false;
                else
                    Console.WriteLine("Invalid input: " + input + ". Try Again");
            }
        }

        public static void Exit()
        {
            Console.WriteLine("Do you want to exit?");
            if (Confirm())
            {
                Console.WriteLine("Exiting program");
                Environment.Exit(0);
            }
        }
    }
}


