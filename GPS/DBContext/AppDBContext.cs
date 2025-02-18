using GPS.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GPS.DBContext{

    public class AppDBContext<T> where T : class {
        
        private readonly IMongoDatabase _database;

        public AppDBContext(IOptions<DBSettings> settings){
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection(){
            return _database.GetCollection<T>($"{typeof(T).Name.Replace("Model", "")}s");
        }
    }
}