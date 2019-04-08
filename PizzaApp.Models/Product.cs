using System;

namespace PizzaApp.Models
{
    public class Product
    {
        public string Name { get; set; }
        public int Cost { get; set; }

        public void Print()
        {
            Console.WriteLine($"{Name} : {Cost}тг");
        }
    }
}
