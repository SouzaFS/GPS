
using GPS.Models;

namespace GPS.GraphQL.Interfaces{

    public interface IUserQuery{

        public Task<List<UserModel>> GetUsers();
        public Task<UserModel> GetUserById(string id);
    }
}