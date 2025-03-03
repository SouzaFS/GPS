namespace GPS.GraphQL.Interfaces{

    public interface IUserQuery{

        public Task<IGraphQLResult> GetUsers();
        public Task<IGraphQLResult> GetUserById(string id);
    }
}