using GPS.GraphQL.Interfaces;
using GPS.GraphQL.Unions;
using GPS.Models;
using GPS.Repositories.Interfaces;
using HotChocolate.Execution;
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
        
        public async Task<IGraphQLResult> GetUsers(){
            
            try{
                var users = await _baseRepository.GetAll().ToListAsync();
                if(users.Count > 0){
                    foreach (var user in users){
                        var location = await _locationQuery.GetLocationByUserId(user.Id);
                        user.Location = ((LocationResult)location).Location;
                    }

                    return new UserListResult(users);
                }

                return new Result("Users not Found", "404");
            }
            catch (Exception e){
                return new Result(e.Message, "500");
            }
            
        }

        public async Task<IGraphQLResult> GetUserById(string id){
            
            try{
                var user = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();
                if (user != null)
                {
                    var location = await _locationQuery.GetLocationByUserId(user.Id);
                    user.Location = ((LocationResult)location).Location;
                    return new UserResult(user);
                }

                return new Result("User not Found", "404");
            }
            catch(Exception e){
                return new Result(e.Message, "500");
            }
        }

    }
}