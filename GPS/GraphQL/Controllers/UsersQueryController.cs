using GPS.GraphQL.Services.Interfaces;
using GPS.GraphQL.Controllers.Interfaces;
using GPS.Models;

namespace GPS.GraphQL.Controllers{

    [ExtendObjectType("Query")]
    public class UsersQueryController{

        private readonly IUserQuery _userQuery;
        public UsersQueryController(IUserQuery userQuery){
            _userQuery = userQuery;
        }

        public async Task<IGraphQLResult> GetUsers(){
            try{
                var users = await _userQuery.GetUsers();
                if(users.Count > 0){
                    return GraphQL<List<UserModel>>.Ok(users);
                }

                return GraphQL<List<UserModel>>.NotFound();

            }
            catch (Exception e){
                return GraphQL<List<UserModel>>.Problem(e.Message);
            }
        }

        public async Task<IGraphQLResult> GetUserById(string id){
            try{
                var user = await _userQuery.GetUserById(id);
                if (user != null){
                    return GraphQL<UserModel>.Ok(user);
                }

                return GraphQL<UserModel>.NotFound();
            }
            catch (Exception e){
                return GraphQL<UserModel>.Problem(e.Message);
            }
        }
    }
}