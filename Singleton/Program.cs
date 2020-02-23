using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = DB.GetInstance();
            db.GetUsers();
            Console.ReadKey();
        }
    }
    class DB
    {
        private DB() {
            client = new MongoClient(
   "mongodb+srv://konstantinUnicorn:8Q5pTFzyHN53PrPY@mongotestcluster-z4btd.gcp.mongodb.net/test?retryWrites=true&w=majority"
            );
            database = client.GetDatabase("test");
        }
        private static DB _instance;
        private static MongoClient client;
        private static IMongoDatabase database;
        public static DB GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DB();
            }
            return _instance;
        }
        public async void GetUsers()
        {
            var collection = database.GetCollection<User>("users");
            var list = await collection.FindSync(x => true).ToListAsync();

            foreach (var person in list)
            {
                Console.WriteLine($"username: {person.name}, email: {person.email}");
            }
        }
    }
   

    internal class User
    {
        public ObjectId _id { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public int balance { get; set; }
        public int __v { get; set; }
    }
}
