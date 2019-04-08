using PizzaApp.DataAccess;
using PizzaApp.Models;
using PizzaApp.Services;
using PizzaApp.Services.SignUp_and_SignIn;
using System;
using System.Collections.Generic;
using static System.Console;


namespace PizzaApp.Console
{
    public class PizzaWeb
    {
        const int MAIN_MENU_CNT = 3;
        const int INNER_MENU_CNT = 2;

        private DbManager dbManager;
        private Loginer loginer;
        private Registration registration;
        private List<User> users;
        private List<Product> products;
        private List<Product> cart;
        private PasswordWriter passwordWriter;

        private int mainMenu = MAIN_MENU_CNT;

        public void Run()
        {
            dbManager = new DbManager();
            loginer = new Loginer();
            registration = new Registration();
            users = new List<User>();
            passwordWriter = new PasswordWriter();
            cart = new List<Product>();

            while (true)
            {
                switch (MainMenu())
                {
                    case 1:
                        {
                            if (LoginMenu())
                            {
                                InnerMenu();
                            }
                            break;
                        }
                    case 2:
                        {
                            users = dbManager.SelectUsers();
                            User newUser;
                            if (registration.TryAddUser(users, out newUser))
                            {
                                WriteLine("Регистрация успешна!");
                                dbManager.InsertUser(newUser);
                            }
                            else
                            {
                                WriteLine("Регистрация прервана.");
                            }
                            break;
                        }
                    case 3:
                        {
                            WriteLine("До свидания!");
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }

        private void BuyMenu()
        {
            products = dbManager.SelectProducts();
            if(products.Count > 0)
            {
                int buyIndex;
                string buyIndexString;

                int buyMenu;
                string buyMenuString;

                double sum = 0;

                bool flag = true;

                while (flag)
                {
                    WriteLine("\n1) Выбрать что-нибудь");
                    WriteLine("2) Просмотреть корзину: ");
                    WriteLine("3) Перейти к оплате: ");
                    
                    buyMenuString = ReadLine();
                    if(int.TryParse(buyMenuString, out buyMenu))
                    {
                        switch (buyMenu)
                        {
                            case 3:
                                flag = false;
                                break;
                            case 2:
                                {
                                    
                                    if(cart.Count == 0)
                                    {
                                        WriteLine("Коризна пустая!");
                                    }
                                    else
                                    {
                                        WriteLine("У вас в корзине: ");
                                        foreach (var p in cart)
                                        {
                                            p.Print();
                                        }
                                    }
                                    break; 
                                }
                            case 1:
                                {
                                    WriteLine("Вы можете купить: ");
                                    for (int i = 0; i < products.Count; i++)
                                    {
                                        WriteLine($"{i + 1}: {products[i].Name} {products[i].Cost}");
                                    }
                                    WriteLine("\nВведите индекс товара:");
                                    buyIndexString = ReadLine();
                                    if (int.TryParse(buyIndexString, out buyIndex))
                                    {
                                        if (buyIndex > 0 && buyIndex <= products.Count)
                                        {
                                            cart.Add(products[buyIndex - 1]);
                                        }
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
                if(cart.Count == 0)
                {
                    WriteLine("Корзина пустая!");

                    return;
                }
                else
                {
                    WriteLine("\nУ вас в корзине: ");
                    foreach(var p in cart)
                    {
                        p.Print();
                        sum += p.Cost;
                    }
                    WriteLine($"\nК оплате: {sum}тг.");
                }
                
            }
            else
            {
                WriteLine("Извините, ничего нет!");
            }
        }

        private int MainMenu()
        {
            WriteLine("\nВыберите действие:");
            WriteLine("1) Вход");
            WriteLine("2) Регистрация");
            WriteLine("3) Выход из приложения");
            if (int.TryParse(ReadLine(), out mainMenu))
            {
                if (mainMenu > 0 && mainMenu <= MAIN_MENU_CNT)
                {
                    return mainMenu;
                }
            }
            return -1;
        }
        private bool LoginMenu()
        {
            string userLogin, userPassword;
            users = dbManager.SelectUsers();

            WriteLine("\nВведите логин");
            userLogin = ReadLine();
            WriteLine("Введите пароль");
            userPassword = passwordWriter.Write();
            if(loginer.Access(users, userLogin, userPassword))
            {
                WriteLine("Добро пожаловать!");
                return true;
            }
            WriteLine("Неверный логин пользователя или пароль!");
            return false;
        }
        private void InnerMenu()
        {           
            while (true) {
                WriteLine("\nВыберите действие");
                WriteLine("1) Заказать");
                WriteLine("2) Выйти из аккаунта");
                int innerMenu;
                string innerMenuStr = ReadLine();
                if (int.TryParse(innerMenuStr, out innerMenu))
                {
                    if (innerMenu > 0 && INNER_MENU_CNT >= innerMenu)
                    {
                        switch (innerMenu)
                        {
                            case 1:
                                BuyMenu();
                                break;
                            case 2:
                                return;
                        }
                    }
                }
            }

        }
    }
}
