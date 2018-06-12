
using Locadora.models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.classes
{
    public static class MongoConnection
    {
        public static MongoClient client { get; set; }
        public static IMongoDatabase db { get; set; }

        public static IMongoCollection<MovieModel> moviecollection { get; set; }
        public static IMongoCollection<EmployeeModel> employeecollection { get; set; }
        public static IMongoCollection<ClientModel> clientcollection { get; set; }
        public static IMongoCollection<WithDrawModel> withdrawcollection { get; set; }
        public static IMongoCollection<GenreModel> genrecollection { get; set; }

        public static void NewConnection(string dest, string database)
        {
            try
            {
                client = new MongoClient(dest);

                db = client.GetDatabase(database);
            }
            catch 
            {
                
            }
        }

        public static void GetMovieCollection(string coll)
        {
            try
            {
                moviecollection = db.GetCollection<MovieModel>(coll);
            }
            catch 
            {
                
            }
        }

        public static void GetWithDrawCollection(string coll)
        {
            try
            {
                withdrawcollection = db.GetCollection<WithDrawModel>(coll);
            }
            catch 
            {

            }
        }

        public static void GetEmployeeCollection(string coll)
        {
            try
            {
                employeecollection = db.GetCollection<EmployeeModel>(coll);
            }
            catch 
            {

            }
        }

        public static void GetClientCollection(string coll)
        {
            try
            {
                clientcollection = db.GetCollection<ClientModel>(coll);
            }
            catch 
            {

            }
        }

        public static void GetGenreCollection(string coll)
        {
            try
            {
                genrecollection = db.GetCollection<GenreModel>(coll);
            }
            catch 
            {

            }
        }
    }
}
