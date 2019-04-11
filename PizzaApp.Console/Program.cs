using PizzaApp.Services;

namespace PizzaApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;

            PizzaApplication pizza = new PizzaApplication();
            pizza.Run();


            System.Console.ReadLine();
        }
    }
}
