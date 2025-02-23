using GPS.Models;

namespace GPS.GraphQL.Interfaces{

    public interface ILocationQuery{
        Task<List<LocationModel>> GetLocations();
        Task<LocationModel> GetLocationById(string id);
    }
}