using MongoDB.Bson.Serialization.Attributes;
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
        public int Id { get; set; }
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

    }
}
