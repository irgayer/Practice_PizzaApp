using System;

namespace PizzaApp.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public void Print()
        {
            Console.WriteLine($"Login       :{Login}");
            Console.WriteLine($"Password    :{Password}");
            Console.WriteLine($"FullName    :{FullName}");
            Console.WriteLine($"PhoneNumber :{PhoneNumber}");
        }
    }
}
