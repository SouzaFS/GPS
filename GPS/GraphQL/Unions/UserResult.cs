using GPS.Models;

namespace GPS.GraphQL.Unions{

    public class UserListResult : IGraphQLResult{
        public List<UserModel> Users { get; set; }

        public UserListResult(List<UserModel> users){
            Users = users;
        }
    }

    public class UserResult : IGraphQLResult{
        public UserModel User { get; set; }

        public UserResult(UserModel user){
            User = user;
        }
    }
}