using GPS.DTOs;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;
using GPS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.Services{

    public class LocationService : ILocationService {

        private readonly IBaseRepository<LocationModel> _baseRepository;

        public LocationService(IBaseRepository<LocationModel> baseRepository){
            _baseRepository = baseRepository;
        }

        public async Task<LocationModel> CreateLocation(LocationDTO locationDTO){
            var location = LocationMapper.FromDTOToModel(locationDTO);
            
            if (location != null){
                return await _baseRepository.CreateAsync(location);
            }
            
            return null;
        }

        public async Task<bool> DeleteLocation(string userId){
            
            var locationModel = await GetLocationByUserId(userId);
            if (locationModel != null){
                await _baseRepository.DeleteAsync(locationModel);
                return true;
            }

            return false;
        }

        public async Task<List<LocationModel>> GetAllLocations(){
            return await _baseRepository.GetAll().ToListAsync();
        }

        public async Task<LocationModel> GetLocationByUserId(string userId){
            var locationModel = await _baseRepository.GetByWhere(a => a.UserId == userId).FirstOrDefaultAsync();

            if (locationModel != null){
                return locationModel;
            }

            return null;
        }

        public async Task<LocationModel> UpdateLocation(string userId, LocationDTO locationDTO){
            
            var locModel = await GetLocationByUserId(userId);
            var id = locModel.Id;
            
            var locationModel = LocationMapper.FromDTOToModel(locationDTO);
            locationModel.Id = id;

            var result = await _baseRepository.UpdateAsync(locationModel);
            
            if (result != null){
                return locationModel;
            }

            return null;
        }


    }
}