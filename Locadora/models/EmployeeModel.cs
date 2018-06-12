using Locadora.classes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.models
{
    public class EmployeeModel
    {
        [BsonId]
        public BsonObjectId Id { get; set; } = ObjectId.GenerateNewId();
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("login")]
        public string Login { get; set; }
        [BsonElement("address")]
        public AddressModel Address { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("birthdate")]
        public DateTime BirthDate { get; set; }
        [BsonElement("role")]
        public string Role { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("hiring_date")]
        public DateTime HiringDate { get; set; }
        [BsonElement("salary")]
        public double Salary { get; set; }
        [BsonElement("straddress")]
        public string StrAddress { get; set; }

        public static bool Save(EmployeeModel client)
        {
            try
            {
                MongoConnection.employeecollection.InsertOneAsync(client);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool Remove(ObjectId id)
        {
            try
            {
                var filter = Builders<EmployeeModel>.Filter.Eq(x => x.Id, id);
                MongoConnection.employeecollection.DeleteOne(filter);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static bool Replace(EmployeeModel employee)
        {
            try
            {
                var filter = Builders<EmployeeModel>.Filter.Eq(x => x.Id, employee.Id);
                MongoConnection.employeecollection.ReplaceOne(filter, employee);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
