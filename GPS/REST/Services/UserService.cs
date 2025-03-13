using GPS.DTOs;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;
using GPS.REST.Services.Interfaces;

namespace GPS.REST.Services
{
    public class UserService : IUserService{

        private readonly IBaseRepository<UserModel> _baseRepository;

        public UserService(IBaseRepository<UserModel> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<UserModel?> CreateUser(UserDTO userDTO)
        {
            if (userDTO != null){
                var userModel = UserMapper.FromDTOToModel(userDTO);
                var user = await _baseRepository.CreateAsync(userModel);
                if (user != null){
                    return user;
                }
            }
            
            return null;
        }

        public async Task<List<UserModel>?> GetUsers()
        {
            var users = await _baseRepository.GetAll();
            if (users != null){
                return users;
            }
            
            return null;
        }

        public async Task<UserModel?> GetUserById(string id)
        {
            var userModel = await _baseRepository.GetByWhere(a => a.Id == id);
            if (userModel != null){
                return userModel;
            }

            return null;
        }

        public async Task<UserModel?> UpdateUser(string id, UserDTO userDTO)
        {
            if (userDTO != null){
                
                var userModel = await GetUserById(id);
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

        public async Task<bool> DeleteUser(string id)
        {
            var userModel = await GetUserById(id);
            if(userModel != null){
                var result = await _baseRepository.DeleteAsync(userModel);    
                return result;
            }

            return false;
            
        }
        
    }

}