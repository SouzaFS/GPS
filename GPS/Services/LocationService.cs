using GPS.DTOs;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;
using GPS.Services.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

        public async Task<bool> DeleteLocation(string id){
            
            var locationModel = await GetLocationById(id);
            if (locationModel != null){
                await _baseRepository.DeleteAsync(locationModel);
                return true;
            }

            return false;
        }

        public async Task<List<LocationModel>> GetLocations(){
            return await _baseRepository.GetAll().ToListAsync();
        }

        public async Task<LocationModel> GetLocationById(string id){
            var locationModel = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();

            if (locationModel != null){
                return locationModel;
            }

            return null;
        }

        public async Task<LocationModel> UpdateLocation(string id, LocationDTO locationDTO){
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