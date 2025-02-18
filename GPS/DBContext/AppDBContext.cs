using GPS.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GPS.DBContext{

    public class AppDBContext<T> where T : class {
        
        private readonly IMongoCollection<T> _collection;

        public AppDBContext(IOptions<DBSettings> settings){
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<T>($"{typeof(T).Name.Replace("Model", "")}s");
        }

        public IMongoCollection<T> GetCollection(){
            return _collection;
        }
    }
}