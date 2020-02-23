using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonString = @"{Name: ""Grisha"", Age:18}";
            var xmlString = @"<root><Name>Alan</Name><Age>22</Age></root>";


            IUserGetter[] workers = { new JSONWorker(), new XMLToJsonAdapter() };
            var user = workers[0].GetUser(jsonString);
            Console.WriteLine($"Name: {user.Name}, age: {user.Age}");

            var user2 = workers[1].GetUser(xmlString);
            Console.WriteLine($"Name: {user2.Name}, age: {user2.Age}");

            Console.ReadKey();
        }
        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        interface IUserGetter
        {
            User GetUser(string jsonString);
        }
        class JSONWorker:IUserGetter
        {
            public User GetUser(string jsonstring)
            {
                var desUser = JsonConvert.DeserializeObject<dynamic>(jsonstring);
                var user = JsonConvert.DeserializeObject<User>(jsonstring);
                return user;
            }
        }
        class XMLToJsonAdapter : IUserGetter
        {
            private JSONWorker jsonWorker;
            public XMLToJsonAdapter()
            {
                jsonWorker = new JSONWorker();
            }
            public User GetUser(string xml)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                return jsonWorker.GetUser(json);
            }

        }
    }
}
