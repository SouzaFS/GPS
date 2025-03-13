using MongoDB.Driver;

namespace GPS.DBContext{

    public interface IAppDBContext<T> where T : class{

        public IMongoCollection<T> GetCollection();
    }
}