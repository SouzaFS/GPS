using GPS.DTOs;
using GPS.GraphQL.Interfaces;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;

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
                        return GraphQLModel<UserModel>.Created(user);
                    }

                    await DeleteUser(user.Id);
                    return GraphQLModel<LocationModel>.BadRequest();
                }
                
                return GraphQLModel<UserModel>.BadRequest();
            }
            catch(Exception e){
                return new GraphQLModel<UserModel>(){
                    Data = null,
                    Code = "500",
                    Message = e.Message,
                    Success = false
                };
            }

        }

        public async Task<IGraphQLResult> UpdateUser(string id, UserDTO userDTO){
            
            try{
                var userModel = UserMapper.FromDTOToModel(userDTO);
                userModel.Id = id;
                var user = await _baseRepository.UpdateAsync(userModel);
                if(user != null){
                    return GraphQLModel<UserModel>.Ok(user);
                }

                return GraphQLModel<UserModel>.BadRequest();
            }
            catch(Exception e){
                return GraphQLModel<UserModel>.Problem(e.Message);
            }
        }

        public async Task<IGraphQLResult> DeleteUser(string id){

            try{
                var user = await _userQuery.GetUserById(id);
                var location = await _locationQuery.GetLocationByUserId(id);
                if (location != null){
                    var locationModel = ((GraphQLModel<LocationModel>)location)?.Data;
                    if (locationModel != null)
                    {
                        await _locationMutation.DeleteLocation(locationModel.Id);
                        var userModel = ((GraphQLModel<UserModel>)user)?.Data;
                        if (userModel != null)
                        {
                            await _baseRepository.DeleteAsync(userModel);
                            return GraphQLModel<UserModel>.NoContent();
                        }
                    }
                }

                return GraphQLModel<UserModel>.UnprocessableEntity();
                
            }
            catch(Exception e){
                return GraphQLModel<UserModel>.Problem(e.Message);
            }

        }

    }
}