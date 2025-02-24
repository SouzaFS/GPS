using GPS.DTOs;
using GPS.GraphQL.Interfaces;
using GPS.Mappers;
using GPS.Models;
using GPS.Repositories.Interfaces;

namespace GPS.GraphQL{

    [ExtendObjectType("Mutation")]
    public class LocationMutation : ILocationMutation{
        private readonly IBaseRepository<LocationModel> _baseRepository;
        private readonly ILocationQuery _locationQuery;

        public LocationMutation(IBaseRepository<LocationModel> locationRepository, ILocationQuery locationQuery){
            _baseRepository = locationRepository;
            _locationQuery = locationQuery;
        }

        public async Task<LocationModel> CreateLocation(LocationDTO locationDTO){
            try{
                var location = LocationMapper.FromDTOToModel(locationDTO);
                if (location != null){
                    return await _baseRepository.CreateAsync(location);
                }

                throw new Exception("Location could not be created");
            }
            catch (Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error creating location");
            }

        }

        public async Task<LocationModel> UpdateLocation(string id, LocationDTO locationDTO){
            try{
                var location = LocationMapper.FromDTOToModel(locationDTO);
                location.Id = id;

                return await _baseRepository.UpdateAsync(location);
            }
            catch (Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error updating location");
            }
            
        }

        public async Task<bool> DeleteLocation(string id){
            try{
                var location = await _locationQuery.GetLocationById(id);
                if (location != null){
                    await _baseRepository.DeleteAsync(location);
                    return true;
                }
                
                return false;
            }
            catch (Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error deleting location");
            }
        }
    }
}