using GPS.DTOs;
using GPS.Models;

namespace GPS.GraphQL.Interfaces{
    public interface ILocationMutation{
        public Task<LocationModel> CreateLocation(LocationDTO locationDTO);
        public Task<LocationModel> UpdateLocation(string id, LocationDTO locationDTO);
        public Task<bool> DeleteLocation(string id);
    }
}