using GPS.Models;

namespace GPS.GraphQL.Interfaces{

    public interface ILocationQuery{
        Task<List<LocationModel>> GetAllLocations();
        Task<LocationModel> GetLocationByUserId(string userId);
    }
}