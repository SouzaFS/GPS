namespace GPS.GraphQL.Interfaces{

    public interface ILocationQuery{
        Task<IGraphQLResult> GetLocations();
        Task<IGraphQLResult> GetLocationById(string id);
        Task<IGraphQLResult> GetLocationByUserId(string userId);
    }
}