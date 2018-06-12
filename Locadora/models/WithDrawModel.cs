using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Locadora.classes;

namespace Locadora.models
{
    public class WithDrawModel
    {
        [BsonId]
        public BsonObjectId Id { get; set; } = ObjectId.GenerateNewId();
        [BsonElement("movie_title")]
        public string MovieTitle { get; set; }
        [BsonElement("clientid")]
        public BsonObjectId ClientId { get; set; }
        [BsonElement("withdrawdate")]
        public DateTime WithDrawDate { get; set; }
        [BsonElement("withdrawreturn")]
        public DateTime WithDrawReturn { get; set; }
        [BsonElement("employeeid")]
        public BsonObjectId Employee { get; set; }

        public static bool Save(WithDrawModel withdraw)
        {
            try
            {
                MongoConnection.withdrawcollection.InsertOneAsync(withdraw);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
