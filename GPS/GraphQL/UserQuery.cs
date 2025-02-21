using GPS.GraphQL.Interfaces;
using GPS.Models;
using GPS.Repositories.Interfaces;
//using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace GPS.GraphQL{

    [QueryType]
    public class UserQuery : IUserQuery{

        private readonly IBaseRepository<UserModel> _baseRepository;

        public UserQuery(IBaseRepository<UserModel> baseRepository){
            _baseRepository = baseRepository;
        }
        
        [UseFiltering]
        [UseProjection]
        public async Task<List<UserModel>> GetUsers(){
            try{
                return await _baseRepository.GetAll().ToListAsync();
            }
            catch(Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error getting users");
            }
        }

        [UseFiltering]
        [UseProjection]
        public async Task<UserModel> GetUserById(string id){
            
            try{
                var user = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();
                if (user != null)
                {
                    return user;
                }

                throw new Exception($"User with id {id} not found.");
            }
            catch(Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error getting user");
            }
        }

    }
}