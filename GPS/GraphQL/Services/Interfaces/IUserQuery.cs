using GPS.Models;

namespace GPS.GraphQL.Services.Interfaces{

    public interface IUserQuery{

        public Task<List<UserModel>?> GetUsers();
        public Task<UserModel?> GetUserById(string id);
    }
}