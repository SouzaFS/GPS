using GPS.DTOs;

namespace GPS.GraphQL.Interfaces{

    public interface IUserMutation{
        public Task<IGraphQLResult> CreateUser(UserDTO userDTO);
        public Task<IGraphQLResult> UpdateUser(string id, UserDTO userDTO);
        public Task<IGraphQLResult> DeleteUser(string id);
    }
}