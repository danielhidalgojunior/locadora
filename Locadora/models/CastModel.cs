using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.models
{
    public class CastModel
    {
        [BsonElement("main")]
        public List<string> Main { get; set; }
        [BsonElement("others")]
        public List<string> Others { get; set; }

        public string GetMain()
        {
            string str = null;

            Main.ForEach(x => str += x + ", ");
            str = str.Substring(0, str.Length - 2);

            return str;
        }

        public string GetOthers()
        {
            string str = null;

            Others.ForEach(x => str += x + ", ");
            str = str.Substring(0, str.Length - 2);

            return str;
        }
    }
}
