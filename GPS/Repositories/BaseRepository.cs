using System.Linq.Expressions;
using System.Threading.Tasks;
using GPS.DBContext;
using GPS.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GPS.Repositories{
    public class BaseRepository <T> : IBaseRepository <T> where T : class
    {
        private readonly AppDBContext<T> _dbContext;
        private readonly IMongoCollection<T> collection;

        public BaseRepository(AppDBContext<T> dbContext){
            _dbContext = dbContext;
            collection = _dbContext.GetCollection();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            var idProperty = entity.GetType().GetProperty("Id");
            if (idProperty == null)
            {
                throw new InvalidOperationException("The entity does not have an 'Id' property.");
            }
            var idValue = idProperty.GetValue(entity);
            if (idValue == null)
            {
                throw new InvalidOperationException("The 'Id' property value is null.");
            }
            
            await collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", idValue));
            
        }

        public async Task<List<T>> GetAll()
        {
            var result = await collection.AsQueryable().ToListAsync();
            return result;
        }

        public async Task<T> GetByWhere(Expression<Func<T, bool>> predicate)
        {
            var result = await collection.AsQueryable().Where(predicate).FirstOrDefaultAsync();
            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var idProperty = entity.GetType().GetProperty("Id");
            if(idProperty == null){
                return null;
            }
            var idValue = idProperty.GetValue(entity);
            
            await collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", idValue), entity);
            return entity;
        }

    }

}