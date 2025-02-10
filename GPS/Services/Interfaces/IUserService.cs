using GPS.DTOs;
using GPS.Models;

namespace GPS.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> CreateUser(UserDTO userDTO);
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetUserById(string id);
        Task<UserModel> UpdateUser(string id, UserDTO userDTO);
        Task<bool> DeleteUser(string id);
        
    }
}