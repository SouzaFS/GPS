using GPS.DTOs;
using GPS.GraphQL.Controllers.Interfaces;
using GPS.GraphQL.Services.Interfaces;
using GPS.Models;

namespace GPS.GraphQL.Controllers{

    [ExtendObjectType("Mutation")]
    public class UsersMutationController{

        private readonly IUserMutation _userMutation;
        public UsersMutationController(IUserMutation userMutation){
            
            _userMutation = userMutation;
        }

        public async Task<IGraphQLResult> CreateUser(UserDTO userDTO){
            
            try{
                var user = await _userMutation.CreateUser(userDTO);
                if (user != null){
                    return GraphQL<UserModel>.Created(user);
                }

                return GraphQL<UserModel>.BadRequest();
            }
            catch(Exception e){
                return GraphQL<UserModel>.Problem(e.Message);
            }
            
        }

        public async Task<IGraphQLResult> UpdateUser(string id, UserDTO userDTO){

            try{
                var user = await _userMutation.UpdateUser(id, userDTO);
                if (user != null){
                    return GraphQL<UserModel>.Ok(user);
                }

                return GraphQL<UserModel>.BadRequest();
            }
            catch(Exception e){
                return GraphQL<UserModel>.Problem(e.Message);
            }
        }

        public async Task<IGraphQLResult> DeleteUser(string id){

            try{
                var result = await _userMutation.DeleteUser(id);
                if (result is true){
                    return GraphQL<UserModel>.NoContent();
                }

                return GraphQL<UserModel>.UnprocessableEntity();
            }
            catch(Exception e){
                return GraphQL<UserModel>.Problem(e.Message);
            }
        }
    }
}