using GPS.GraphQL.Interfaces;
using GPS.Models;
using GPS.Repositories.Interfaces;
using MongoDB.Driver.Linq;

namespace GPS.GraphQL{

    [ExtendObjectType("Query")]
    public class LocationQuery : ILocationQuery{

        private readonly IBaseRepository<LocationModel> _baseRepository;

        public LocationQuery(IBaseRepository<LocationModel> baseRepository){
            _baseRepository = baseRepository;
        }

        public async Task<IGraphQLResult> GetLocations(){
            try{
                var locations = await _baseRepository.GetAll().ToListAsync();
                if (locations.Count > 0)
                {
                    return GraphQLModel<List<LocationModel>>.Ok(locations);
                }

                return GraphQLModel<List<LocationModel>>.NotFound();
            }
            catch(Exception e){
                return GraphQLModel<List<LocationModel>>.Problem(e.Message);
            }
        }
        public async Task<IGraphQLResult> GetLocationById(string id){
            
            try{
                var location = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();
                if (location != null){
                    return GraphQLModel<LocationModel>.Ok(location);
                }

                return GraphQLModel<LocationModel>.NotFound();
            }
            catch(Exception e){
                return GraphQLModel<LocationModel>.Problem(e.Message);
            }
        }

        public async Task<IGraphQLResult> GetLocationByUserId(string userId){
            try{
                var location = await _baseRepository.GetByWhere(a => a.UserId == userId).FirstOrDefaultAsync();
                if (location != null){
                    return GraphQLModel<LocationModel>.Ok(location);
                }

                return GraphQLModel<LocationModel>.NotFound();
            }
            catch(Exception e){
                return GraphQLModel<LocationModel>.Problem(e.Message);
            }
        }
    }
}