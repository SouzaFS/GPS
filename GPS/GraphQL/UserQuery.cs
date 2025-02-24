using GPS.GraphQL.Interfaces;
using GPS.Models;
using GPS.Repositories.Interfaces;
using MongoDB.Driver.Linq;

namespace GPS.GraphQL{

    [ExtendObjectType("Query")]
    public class UserQuery : IUserQuery{

        private readonly IBaseRepository<UserModel> _baseRepository;

        private readonly ILocationQuery _locationQuery;

        public UserQuery(IBaseRepository<UserModel> baseRepository, ILocationQuery locationQuery){
            _locationQuery = locationQuery;
            _baseRepository = baseRepository;
        }
        
        public async Task<List<UserModel>> GetUsers(){
            try{
                var users = await _baseRepository.GetAll().ToListAsync();
                if (users.Count > 0)
                {
                    foreach (var user in users){
                        user.Location = await _locationQuery.GetLocationById(user.LocationId);
                    }
                    return users;
                }

                throw new Exception("No users found.");
            }
            catch(Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error getting users");
            }
        }

        public async Task<UserModel> GetUserById(string id){
            
            try{
                var user = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.Location = await _locationQuery.GetLocationById(user.LocationId);
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