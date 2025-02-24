using GPS.DTOs;
using GPS.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPS.GraphQL.Interfaces{

    public interface IUserMutation{
        public Task<UserModel> CreateUser(UserDTO userDTO);
        public Task<UserModel> UpdateUser(string id, UserDTO userDTO);
        public Task<bool> DeleteUser(string id);
    }
}