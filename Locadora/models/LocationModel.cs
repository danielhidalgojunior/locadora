using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.models
{
    public class LocationModel
    {
        [BsonElement("stand")]
        public char Stand { get; set; }
        [BsonElement("shelf")]
        public byte Shelf { get; set; }
        [BsonElement("pos")]
        public int Pos { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}, {2}", Stand, Shelf, Pos);
        }
    }
}
