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
        public UserMutation(IBaseRepository<UserModel> baseRepository, IUserQuery userQuery){
            _userQuery = userQuery;
            _baseRepository = baseRepository;
        }
        public async Task<UserModel> CreateUser(UserDTO userDTO){
            
            var userModel = UserMapper.FromDTOToModel(userDTO);
            return await _baseRepository.CreateAsync(userModel);

        }

        public async Task<UserModel> UpdateUser(string id, UserDTO userDTO){
            
            var userModel = UserMapper.FromDTOToModel(userDTO);
            userModel.Id = id;

            return await _baseRepository.UpdateAsync(userModel);
        }

        public async Task DeleteUser(string id){
            
            var user = await _userQuery.GetUserById(id);
            await _baseRepository.DeleteAsync(user);

        }
    }
}