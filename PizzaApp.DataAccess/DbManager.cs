using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

namespace PizzaApp.DataAccess
{
    public class DbManager
    {
        private readonly string connectionString;
        private readonly string providerName;
        private readonly DbProviderFactory providerFactory;

        private DbConnection connection;

        public DbManager()
        {
            connectionString = ConfigurationManager.ConnectionStrings["appConnectionString"].ConnectionString;
            providerName = ConfigurationManager.ConnectionStrings["appConnectionString"].ProviderName;
            providerFactory = DbProviderFactories.GetFactory(providerName);
        }

        public void InsertUser(User newUser)
        {
            using (var connection = providerFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    command.CommandText = $"insert into Users(Login,Password,FullName,PhoneNumber) values(@login, @password, @fullname, @phone)";

                    var loginParameter = command.CreateParameter();
                    loginParameter.ParameterName = "@login";
                    loginParameter.DbType = System.Data.DbType.String;
                    loginParameter.Value = newUser.Login;

                    var passwordParameter = command.CreateParameter();
                    passwordParameter.ParameterName = "@password";
                    passwordParameter.DbType = System.Data.DbType.String;
                    passwordParameter.Value = newUser.Password;


                    var fullnameParameter = command.CreateParameter();
                    fullnameParameter.ParameterName = "@fullname";
                    fullnameParameter.DbType = System.Data.DbType.String;
                    fullnameParameter.Value = newUser.FullName;

                    var phoneParameter = command.CreateParameter();
                    phoneParameter.ParameterName = "@phone";
                    phoneParameter.DbType = System.Data.DbType.String;
                    phoneParameter.Value = newUser.PhoneNumber;

                    command.Parameters.Add(loginParameter);
                    command.Parameters.Add(passwordParameter);
                    command.Parameters.Add(fullnameParameter);
                    command.Parameters.Add(phoneParameter);

                    int affectedRows = command.ExecuteNonQuery();
                    if (affectedRows < 1)
                    {
                        throw new Exception("Вставка не удалась!");
                    }
                }
                catch (DbException exception)
                {
                    //обработать
                    throw;
                }
                catch (Exception exception)
                {
                    //обработать
                    throw;
                }
            }
        }
        public void InsertProduct(Product newProduct)
        {
            using (var connection = providerFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    command.CommandText = $"insert into Products(Name,Cost) values(@name, @cost)";

                    var nameParameter = command.CreateParameter();
                    nameParameter.ParameterName = "@name";
                    nameParameter.DbType = System.Data.DbType.String;
                    nameParameter.Value = newProduct.Name;

                    var costParameter = command.CreateParameter();
                    costParameter.ParameterName = "@cost";
                    costParameter.DbType = System.Data.DbType.String;
                    costParameter.Value = newProduct.Cost;

                    command.Parameters.Add(nameParameter);
                    command.Parameters.Add(costParameter);

                    int affectedRows = command.ExecuteNonQuery();
                    if(affectedRows < 1)
                    {
                        throw new Exception("Вставка не удалась!");
                    }

                }
                catch (DbException exception)
                {
                    //обработать
                    throw;
                }
                catch (Exception exception)
                {
                    //обработать
                    throw;
                }
            }
        }
        public List<User> SelectUsers()
        {
            var result = new List<User>();
            using (var connection = providerFactory.CreateConnection())
            using(var command = connection.CreateCommand())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    command.CommandText = "select * from Users";

                    var sqlDataReader = command.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        string login = sqlDataReader["Login"].ToString();
                        string password = sqlDataReader["Password"].ToString();
                        string fullname = sqlDataReader["FullName"].ToString();
                        string phoneNumber = sqlDataReader["PhoneNumber"].ToString();


                        result.Add(new User
                        {
                            Login = login,
                            Password = password,
                            FullName = fullname,
                            PhoneNumber = phoneNumber
                        });
                    }
                    return result;
                }
                catch (DbException exception)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    throw;
                }
            }
        }
        public List<Product> SelectProducts()
        {
            var result = new List<Product>();
            using (var connection = providerFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    command.CommandText = "select * from Products";

                    var sqlDataReader = command.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        string name = sqlDataReader["Name"].ToString();
                        int cost = int.Parse(sqlDataReader["Cost"].ToString());

                        result.Add(new Product
                        {
                            Name = name,
                            Cost = cost
                        });
                    }
                    return result;
                }
                catch (DbException exception)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    throw;
                }
            }
        }
        
        
    }
}
