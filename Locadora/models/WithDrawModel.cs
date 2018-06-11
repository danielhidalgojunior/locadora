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
        public BsonObjectId Id = ObjectId.GenerateNewId();
        [BsonElement("movie_title")]
        public string MovieTitle { get; set; }
        [BsonId]
        public BsonObjectId ClientId { get; set; }
        [BsonElement("withdrawdate")]
        public DateTime WithDrawDate { get; set; }
        [BsonElement("withdrawreturn")]
        public DateTime WithDrawReturn { get; set; }
        [BsonId]
        public BsonObjectId Employee { get; set; }
    }
}
