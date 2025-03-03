using GPS.DTOs;
using GPS.GraphQL.Interfaces;
using GPS.GraphQL.Unions;
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

        public async Task<IGraphQLResult> CreateLocation(LocationDTO locationDTO){
            try{
                var location = LocationMapper.FromDTOToModel(locationDTO);
                if (location != null){
                    var result = await _baseRepository.CreateAsync(location);
                    return new LocationResult(result);
                }

                return new Result("Location could not be created", "400");
            }
            catch (Exception e){
                return new Result(e.Message, "500");
            }

        }

        public async Task<IGraphQLResult> UpdateLocation(string id, LocationDTO locationDTO){
            try{
                var userId = ((LocationResult)await _locationQuery.GetLocationById(id)).Location.UserId;
                var location = LocationMapper.FromDTOToModel(locationDTO);
                location.Id = id;
                location.UserId = userId;
                if(userId != null){
                    var result = await _baseRepository.UpdateAsync(location);
                    return new LocationResult(result);
                }

                return new Result("Location could not be Updated", "400");
            }
            catch (Exception e){
                return new Result(e.Message, "500");
            }
            
        }

        public async Task<IGraphQLResult> DeleteLocation(string id){
            try{
                var location = await _locationQuery.GetLocationById(id);
                if (location != null){
                    await _baseRepository.DeleteAsync(((LocationResult)location).Location);
                    return new Result("No Content", "204");
                }
                
                return new Result("Location Could not be Deleted", "422");
            }
            catch (Exception e){
                return new Result(e.Message, "500");
            }
        }
    }
}