using GPS.DTOs;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;
using GPS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.Services
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
            return await _baseRepository.GetAll().ToListAsync();
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
            var userModel = UserMapper.FromDTOToModel(userDTO);
            userModel.Id = id;
            
            var result = await _baseRepository.UpdateAsync(userModel);
            if (result != null){
                return userModel;
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