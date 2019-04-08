using System;

namespace PizzaApp.Services
{
    public class PasswordWriter
    {
        public string Write()
        {
            string result = string.Empty;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    result += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && result.Length > 0)
                    {
                        result = result.Substring(0, (result.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        break;
                    }
                }
            } while (true);
            return result;
        }
    }
}
