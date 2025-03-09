using GPS.DTOs;
using GPS.GraphQL.Services.Interfaces;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;
using ZstdSharp.Unsafe;

namespace GPS.GraphQL.Services{

    public class UserMutation : IUserMutation{

        private readonly IBaseRepository<UserModel> _baseRepository;
        private readonly IUserQuery _userQuery;

        public UserMutation(IBaseRepository<UserModel> baseRepository, IUserQuery userQuery){
            _userQuery = userQuery;
            _baseRepository = baseRepository;
        }

        public async Task<UserModel?> CreateUser(UserDTO userDTO){
            
            if (userDTO != null){
                var userModel = UserMapper.FromDTOToModel(userDTO);
                var user = await _baseRepository.CreateAsync(userModel);
                if (user != null){
                    return user;
                }
            }

            return null;
        }

        public async Task<UserModel?> UpdateUser(string id, UserDTO userDTO){
            
            if (userDTO != null){
                
                var userModel = await _userQuery.GetUserById(id);
                if (userModel != null){
                    
                    var user = UserMapper.FromDTOToExistingModel(userModel, userDTO);
                    var result = await _baseRepository.UpdateAsync(user);
                    if (result != null){
                        return result;
                    }
                }
            }

            return null;
        }

        public async Task<bool> DeleteUser(string id){
                
            var user = await _userQuery.GetUserById(id);
            if (user != null)
            {
                await _baseRepository.DeleteAsync(user);
                return true;
            }
                
            return false;
        }

    }
}