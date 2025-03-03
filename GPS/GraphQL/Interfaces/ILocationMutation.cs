using GPS.DTOs;
using GPS.GraphQL.Unions;

namespace GPS.GraphQL.Interfaces{
    public interface ILocationMutation{
        public Task<IGraphQLResult> CreateLocation(LocationDTO locationDTO);
        public Task<IGraphQLResult> UpdateLocation(string id, LocationDTO locationDTO);
        public Task<IGraphQLResult> DeleteLocation(string id);
    }
}