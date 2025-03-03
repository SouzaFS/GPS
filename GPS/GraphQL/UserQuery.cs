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

        public async Task<IGraphQLResult> GetUsers(){
            
            try{
                var users = await _baseRepository.GetAll().ToListAsync();
                if(users.Count > 0){
                    foreach (var user in users){
                        var location = await _locationQuery.GetLocationByUserId(user.Id);
                        user.Location = ((GraphQLModel<LocationModel>)location).Data;
                    }

                    return GraphQLModel<List<UserModel>>.Ok(users);
                }

                return GraphQLModel<List<UserModel>>.NotFound();
            }
            catch (Exception e){
                return GraphQLModel<List<UserModel>>.Problem(e.Message);
            }
            
        }

        public async Task<IGraphQLResult> GetUserById(string id){
            
            try{
                var user = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();
                if (user != null)
                {
                    var location = await _locationQuery.GetLocationByUserId(user.Id);
                    user.Location = ((GraphQLModel<LocationModel>)location).Data;
                    
                    return GraphQLModel<UserModel>.Ok(user);
                }

                return GraphQLModel<UserModel>.NotFound();
            }
            catch(Exception e){
                return GraphQLModel<UserModel>.Problem(e.Message);
            }
        }
        

    }
}