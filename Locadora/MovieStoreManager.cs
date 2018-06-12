using Locadora.models;
using Locadora.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Locadora
{
    public static class MovieStoreManager
    {
        // Coração do sistema, aqui é onde tudo fica armazenado para seja usado em diversas telas diferentes

        public static EmployeeModel LoggedEmployee { get; set; }

        public static List<MovieModel> Movies { get; set; }
        public static List<EmployeeModel> Employees { get; set; }
        public static List<WithDrawModel> WithDraws { get; set; }
        public static GenreModel Genres { get; set; }
        public static List<ClientModel> Clients { get; set; }

        public static bool Initialized = false;

        // Carrega todos os filmes
        public static List<MovieModel> GetAllMovies()
        {
            try
            {
                var filter = Builders<MovieModel>.Filter.Empty;

                return MongoConnection.moviecollection.Find(filter).ToList();
            }
            catch
            {
                throw;
            }
        }

        // Carrega todos os Funcionarios
        public static List<EmployeeModel> GetAllEmployees()
        {
            try
            {
                var filter = Builders<EmployeeModel>.Filter.Empty;

            return MongoConnection.employeecollection.Find(filter).ToList();
            }
            catch
            {
                return null;
            }
        }

        // Carrega todos os alugueis e retiradas
        public static List<WithDrawModel> GetAllWithDraws()
        {
            try
            {
                var filter = Builders<WithDrawModel>.Filter.Empty;

                return MongoConnection.withdrawcollection.Find(filter).ToList();
            }
            catch
            {
                return null;
            }
        }

        // Carrega todos os generos
        public static GenreModel GetAllGenres()
        {
            try
            {
                var filter = Builders<GenreModel>.Filter.Empty;

                return MongoConnection.genrecollection.Find(filter).Single();
            }
            catch
            {
                return null;
            }
        }

        // Carrega todos os clientes
        public static List<ClientModel> GetAllClients()
        {
            try
            {
                var filter = Builders<ClientModel>.Filter.Empty;

                return MongoConnection.clientcollection.Find(filter).ToList();
            }
            catch
            {
                return null;
            }
        }

        // Função que verifica as credenciais do funcionario para realizar login
        public static bool TryLogin(string login, string pw)
        {
            try
            {
                var filter = Builders<EmployeeModel>.Filter.Eq(x => x.Login, login.ToLower());

                var employee = MongoConnection.employeecollection.Find(filter).Single();

                if (employee.Password == pw)
                {
                    LoggedEmployee = employee;
                }

                return employee.Password == pw;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        // Função que iniciliza as informações do banco
        public static void Initialize()
        {
            dynamic settings = JsonConfig.FromFile(@"config/config.json");
            string dbhost = (string)settings["databasehost"];
            string dbname = (string)settings["databasename"];


            MongoConnection.NewConnection(dbhost, dbname);
            MongoConnection.GetMovieCollection("movies");
            MongoConnection.GetClientCollection("clients");
            MongoConnection.GetEmployeeCollection("employees");
            MongoConnection.GetGenreCollection("genres");
            MongoConnection.GetWithDrawCollection("withdraws");

            Movies = GetAllMovies();
            //Employees = GetAllEmployees();
            //WithDraws = GetAllWithDraws();
            Genres = GetAllGenres();
            //Clients = GetAllClients();

            Initialized = true;
        }
    }
}
