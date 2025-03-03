using GPS.DTOs;
using GPS.GraphQL.Interfaces;
using GPS.GraphQL.Unions;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;
using HotChocolate.Resolvers;

namespace GPS.GraphQL{

    [ExtendObjectType("Mutation")]
    public class UserMutation : IUserMutation{

        private readonly IBaseRepository<UserModel> _baseRepository;
        private readonly IUserQuery _userQuery;

        private readonly ILocationMutation _locationMutation;
        private readonly ILocationQuery _locationQuery;
        public UserMutation(IBaseRepository<UserModel> baseRepository, IUserQuery userQuery, ILocationMutation locationMutation, ILocationQuery locationQuery){
            _locationQuery = locationQuery;
            _locationMutation = locationMutation;
            _userQuery = userQuery;
            _baseRepository = baseRepository;
        }
        public async Task<IGraphQLResult> CreateUser(UserDTO userDTO){
            
            try{
                var userModel = UserMapper.FromDTOToModel(userDTO);
                var user = await _baseRepository.CreateAsync(userModel);
                if (user != null){
                    var location = await _locationMutation.CreateLocation(new LocationDTO{
                        UserId = user.Id
                    });
                    if (location != null){
                        return new UserResult(user);
                    }

                    await DeleteUser(user.Id);
                    return new Result("Location could not be created", "400");  
                }
                
                return new Result("User could not be created", "400");
            }
            catch(Exception e){
                return new Result(e.Message, "500");
            }

        }

        public async Task<IGraphQLResult> UpdateUser(string id, UserDTO userDTO){
            
            try{
                var userModel = UserMapper.FromDTOToModel(userDTO);
                userModel.Id = id;
                var user = await _baseRepository.UpdateAsync(userModel);
                if(user != null){
                    return new UserResult(user);
                }

                return new Result("User could not be Updated", "400");
            }
            catch(Exception e){
                return new Result(e.Message, "500");
            }
        }

        public async Task<IGraphQLResult> DeleteUser(string id){

            try{
                var userResult = await _userQuery.GetUserById(id);
                var location = await _locationQuery.GetLocationByUserId(id);
                if (location != null){
                    await _locationMutation.DeleteLocation(((LocationResult)location).Location.Id);
                }

                if (userResult.GetType() == typeof(UserResult)){
                    await _baseRepository.DeleteAsync(((UserResult)userResult).User);
                    return new Result("No Content","204");
                }

                return new Result("User Could not be Deleted", "422");
                
            }
            catch(Exception e){
                return new Result(e.Message, "500");
            }

        }
    }
}