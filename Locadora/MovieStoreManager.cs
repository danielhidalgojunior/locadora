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
        public static EmployeeModel LoggedEmployee { get; set; }

        public static List<MovieModel> Movies { get; set; }
        public static List<EmployeeModel> Employees { get; set; }
        public static List<WithDrawModel> WithDraws { get; set; }
        public static GenreModel Genres { get; set; }
        public static List<ClientModel> Clients { get; set; }

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

        public static EmployeeModel GetEmployee(string id)
        {
            var bsonid = new ObjectId(id);
            try
            {
                var filter = Builders<EmployeeModel>.Filter.Eq(x => x.Id, bsonid);

                return MongoConnection.employeecollection.Find(filter).Single();
            }
            catch
            {
                return null;
            }
        }

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

        public static GenreModel GetAllGenres()
        {
            try
            {
                var filter = Builders<GenreModel>.Filter.Empty;

                return MongoConnection.genrecollection.Find(filter).Single();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

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

        public static bool TryLogin(string user, string pw)
        {
            return true;
        }

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
            Employees = GetAllEmployees();
            WithDraws = GetAllWithDraws();
            Genres = GetAllGenres();
            Clients = GetAllClients();
        }
    }
}
