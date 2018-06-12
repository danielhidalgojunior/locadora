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

        public override string ToString()
        {
            return string.Format("{0}, bairro {1}, rua {2}, número {3}",City, District, Street, Number);
        }
    }
}
