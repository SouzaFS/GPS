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

        public async Task<IGraphQLResult> CreateLocation(LocationDTO locationDTO){
            try{
                var location = LocationMapper.FromDTOToModel(locationDTO);
                var result = await _baseRepository.CreateAsync(location);
                if (result != null){
                    return GraphQLModel<LocationModel>.Ok(location);
                }

                return GraphQLModel<LocationModel>.BadRequest();
            }
            catch (Exception e){
                return GraphQLModel<LocationModel>.Problem(e.Message);
            }

        }

        public async Task<IGraphQLResult> UpdateLocation(string id, LocationDTO locationDTO){
            try{
                var locationResult = (GraphQLModel<UserModel>)await _locationQuery.GetLocationById(id);
                if (locationResult?.Data?.Id == null)
                {
                    return GraphQLModel<LocationModel>.BadRequest();
                }
                var userId = locationResult.Data.Id;

                var location = LocationMapper.FromDTOToModel(locationDTO);
                location.Id = id;
                location.UserId = userId;

                if(userId != null){
                    var result = await _baseRepository.UpdateAsync(location);
                    return GraphQLModel<LocationModel>.Ok(location);
                }

                return GraphQLModel<LocationModel>.BadRequest();
            }
            catch (Exception e){
                return GraphQLModel<LocationModel>.Problem(e.Message);
            }
            
        }

        public async Task<IGraphQLResult> DeleteLocation(string id){
            try{
                var location = await _locationQuery.GetLocationById(id);
                if (location != null){
                    var locationModel = ((GraphQLModel<LocationModel>)location).Data;
                    if (locationModel != null)
                    {
                        await _baseRepository.DeleteAsync(locationModel);
                        return GraphQLModel<LocationModel>.NoContent();
                    }
                    
                }
                
                return GraphQLModel<LocationModel>.UnprocessableEntity();
            }
            catch (Exception e){
                return GraphQLModel<LocationModel>.Problem(e.Message);
            }
        }
    }
}