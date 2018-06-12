using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.classes
{
    public static class JsonConfig
    {
        // Carrega as informaões no arquivo de texto config.json
        public static JObject FromFile(string file)
        {
            var rawjson = File.ReadAllText(file);

           return (JObject)JsonConvert.DeserializeObject(rawjson);
        }
    }
}
