using GPS.DTOs;
using GPS.GraphQL.Interfaces;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;

namespace GPS.GraphQL{

    [ObjectType("Mutation")]
    public class UserMutation : IUserMutation{

        private readonly IBaseRepository<UserModel> _baseRepository;
        private readonly IUserQuery _userQuery;
        public UserMutation(IBaseRepository<UserModel> baseRepository, IUserQuery userQuery){
            _userQuery = userQuery;
            _baseRepository = baseRepository;
        }
        public async Task<UserModel> CreateUser(UserDTO userDTO){
            
            try{
                var userModel = UserMapper.FromDTOToModel(userDTO);
                return await _baseRepository.CreateAsync(userModel);
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