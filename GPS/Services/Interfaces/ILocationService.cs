using GPS.DTOs;
using GPS.Models;

namespace GPS.Services.Interfaces{
    
    public interface ILocationService{
        Task<LocationModel> CreateLocation(LocationDTO locationDTO);
        Task<List<LocationModel>> GetLocations();
        Task<LocationModel> GetLocationById(string id);
        Task<LocationModel> UpdateLocation(string id, LocationDTO locationDTO);
        Task<bool> DeleteLocation(string id);
    }
}