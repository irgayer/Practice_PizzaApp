using System;

namespace PizzaApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;

            PizzaWeb pizza = new PizzaWeb();
            pizza.Run();

            System.Console.ReadLine();
        }
    }
}
