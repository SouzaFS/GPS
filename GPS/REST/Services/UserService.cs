using GPS.DTOs;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;
using GPS.REST.Services.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GPS.REST.Services
{
    public class UserService : IUserService{

        private readonly IBaseRepository<UserModel> _baseRepository;

        public UserService(IBaseRepository<UserModel> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<UserModel> CreateUser(UserDTO userDTO)
        {
            var userModel = UserMapper.FromDTOToModel(userDTO);
            
            if (userModel != null){
                return await _baseRepository.CreateAsync(userModel);
            }

            return null;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var users = await _baseRepository.GetAll().ToListAsync();
            return users;
        }

        public async Task<UserModel> GetUserById(string id)
        {
            var userModel = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();
            
            if (userModel != null){
                return userModel;
            }

            return null;
        }

        public async Task<UserModel> UpdateUser(string id, UserDTO userDTO)
        {
            var userModel = await GetUserById(id);
            var user = UserMapper.FromDTOToExistingModel(userModel, userDTO);
            
            var result = await _baseRepository.UpdateAsync(user);
            if (result != null){
                return result;
            }

            return null;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var userModel = await GetUserById(id);
            if(userModel != null){
                await _baseRepository.DeleteAsync(userModel);
                return true;
            }

            return false;
            
        }
        
    }
}