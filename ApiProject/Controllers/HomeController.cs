using ApiProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ApiProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration config)
        {
            configuration = config;
        }

        [HttpGet]
        public List<MyDetails> Api()
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
            IMongoDatabase database = client.GetDatabase("myDB");
            IMongoCollection<MyDetails> myDetails = database.GetCollection<MyDetails>("myDetails");
            MyDetails myFirstDetails = new MyDetails
            {
                _id = ObjectId.GenerateNewId(),
                name = "Hari",
                email = "harikrrish27@grr.la"
            };

            myDetails.InsertOne(myFirstDetails);

            List<MyDetails> collection = myDetails.Find(new BsonDocument()).ToList();
            //myDetails.UpdateOne(Builders<MyDetails>.Filter.Eq("_id", collection[0]._id), Builders <MyDetails>.Update.Set("name", "Done"));
            return collection;
        }
    }
}
