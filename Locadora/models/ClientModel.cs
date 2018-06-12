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
    public class ClientModel
    {
        [BsonId]
        public BsonObjectId Id { get; set; } = ObjectId.GenerateNewId();
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("birthdate")]
        public DateTime BirthDate { get; set; }
        [BsonElement("address")]
        public AddressModel Address { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("straddress")]
        public string StrAddress { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static bool Save(ClientModel client)
        {
            try
            {
                MongoConnection.clientcollection.InsertOneAsync(client);
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
                var filter = Builders<ClientModel>.Filter.Eq(x => x.Id, id);
                MongoConnection.clientcollection.DeleteOne(filter);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static bool Replace(ClientModel employee)
        {
            try
            {
                var filter = Builders<ClientModel>.Filter.Eq(x => x.Id, employee.Id);
                MongoConnection.clientcollection.ReplaceOne(filter, employee);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
