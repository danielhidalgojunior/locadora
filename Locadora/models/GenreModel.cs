using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Locadora.models
{
    public class GenreModel
    {
        [BsonId]
        public BsonObjectId Id = ObjectId.GenerateNewId();
        [BsonElement("genres")]
        public List<string> Genres { get; set; }
    }
}
