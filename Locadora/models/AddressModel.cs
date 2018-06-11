using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.models
{
    public class AddressModel
    {
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("district")]
        public string District { get; set; }
        [BsonElement("street")]
        public string Street { get; set; }
        [BsonElement("number")]
        public int Number { get; set; }
    }
}
