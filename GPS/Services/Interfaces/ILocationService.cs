using GPS.DTOs;
using GPS.Models;

namespace GPS.Services.Interfaces{
    
    public interface ILocationService{
        Task<LocationModel> CreateLocation(LocationDTO locationDTO);
        Task<List<LocationModel>> GetAllLocations();
        Task<LocationModel> GetLocationByUserId(string userId);
        Task<LocationModel> UpdateLocation(string userId, LocationDTO locationDTO);
        Task<bool> DeleteLocation(string userId);
    }
}