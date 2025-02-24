using GPS.DTOs;
using GPS.GraphQL.Interfaces;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GPS.GraphQL{

    [ExtendObjectType("Mutation")]
    public class UserMutation : IUserMutation{

        private readonly IBaseRepository<UserModel> _baseRepository;
        private readonly IUserQuery _userQuery;

        private readonly ILocationMutation _locationMutation;
        public UserMutation(IBaseRepository<UserModel> baseRepository, IUserQuery userQuery, ILocationMutation locationMutation){
            _locationMutation = locationMutation;
            _userQuery = userQuery;
            _baseRepository = baseRepository;
        }
        public async Task<UserModel> CreateUser(UserDTO userDTO){
            
            try{
                var location = await _locationMutation.CreateLocation(new LocationDTO());
                if (location != null){
                    var userModel = UserMapper.FromDTOToModel(userDTO);
                    userModel.LocationId = location.Id;
                    await _baseRepository.CreateAsync(userModel);
                    if (userModel != null){
                        return userModel;
                    }

                    await _locationMutation.DeleteLocation(location.Id);
                    throw new Exception("User could not be created");
                }

                throw new Exception("Location could not be created");  
            }
            catch(Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error creating user");
            }

        }

        public async Task<UserModel> UpdateUser(string id, UserDTO userDTO){
            
            try{
                var userModel = UserMapper.FromDTOToModel(userDTO);
                userModel.Id = id;
                return await _baseRepository.UpdateAsync(userModel);
            }
            catch(Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error updating user");
            }
        }

        public async Task<bool> DeleteUser(string id){

            try{
                var user = await _userQuery.GetUserById(id);
                if (user != null){
                    await _locationMutation.DeleteLocation(user.LocationId);
                    await _baseRepository.DeleteAsync(user);
                    return true;
                }
                return false;
                
            }
            catch(Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error deleting user");
            }

        }
    }
}