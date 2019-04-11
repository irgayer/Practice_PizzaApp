using PizzaApp.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Console;

namespace PizzaApp.Services.SignUp_and_SignIn
{
    public class Registration
    {
        const int MIN_PSSWD_LEN = 6;
        public bool TryAddUser(List<User> users, out User newUser)
        {
            PasswordWriter passwordWriter = new PasswordWriter();
            SmsSender smsSender = new SmsSender();
            newUser = new User();
          
            string usLoginStr, usPsswdStr, usFullNameStr, usPhoneStr;

            WriteLine("Новый пользователь,");
            WriteLine("Введите логин:");

            usLoginStr = ReadLine();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Login == usLoginStr)
                {
                    WriteLine("Логин уже занят!");
                    return false;
                }
            }

            if (CheckUsername(usLoginStr))
            {
                WriteLine($"Введите пароль(больше {MIN_PSSWD_LEN} символов):");
                usPsswdStr = passwordWriter.Write();

                if (CheckPassword(usPsswdStr))
                {
                    WriteLine("Введите полное имя:");
                    usFullNameStr = ReadLine();

                    WriteLine("Введите номер телефона:");
                    usPhoneStr = ReadLine();

                    if (CheckPhoneNumber(usPhoneStr))
                    {
                        string verification = smsSender.SendSms(usPhoneStr);
                        WriteLine("Введите код авторизации:");
                        if (verification == ReadLine())
                        {
                            newUser.Login = usLoginStr.Trim();
                            newUser.Password = usPsswdStr;
                            newUser.FullName = usFullNameStr;
                            newUser.PhoneNumber = usPhoneStr;

                            return true;
                        }
                        else WriteLine("Неверный код авторизации!");
                    }
                    else WriteLine("Неверный формат телефона!");
                }
                else WriteLine("Пароль недостаточно длинный!");
            }
            else WriteLine("Логин пустой!");

            return false;
        }
        private bool CheckUsername(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                return true;
            }
            return false;
        }
        private bool CheckPassword(string userPsswd)
        {
            if (!string.IsNullOrWhiteSpace(userPsswd))
            {
                if (userPsswd.Length > MIN_PSSWD_LEN)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckFullName(string userFullName)
        {
            if (!string.IsNullOrWhiteSpace(userFullName))
            {
                return true;
            }
            return false;
        }

        private static bool CheckPhoneNumber(string userPhone)
        {
            var phoneNumber = userPhone.Trim()
            .Replace(" ", "")
            .Replace("-", "")
            .Replace("(", "")
            .Replace(")", "");
            return Regex.Match(phoneNumber, @"^\+\d{5,15}$").Success;
        }

    }
}