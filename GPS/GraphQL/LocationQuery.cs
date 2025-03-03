using GPS.GraphQL.Interfaces;
using GPS.GraphQL.Unions;
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
                    return new LocationListResult(locations);
                }

                return new Result("Locations not Found", "404");
            }
            catch(Exception e){
                return new Result(e.Message, "500");
            }
        }
        public async Task<IGraphQLResult> GetLocationById(string id){
            
            try{
                var location = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();
                if (location != null)
                {
                    return new LocationResult(location);
                }

                return new Result("Location not Found", "404");
            }
            catch(Exception e){
                return new Result(e.Message, "500");
            }
        }

        public async Task<IGraphQLResult> GetLocationByUserId(string userId){
            try{
                var location = await _baseRepository.GetByWhere(a => a.UserId == userId).FirstOrDefaultAsync();
                if (location != null)
                {
                    return new LocationResult(location);
                }

                return new Result($"User Location not Found", "404");
            }
            catch(Exception e){
                return new Result(e.Message, "500");
            }
        }
    }
}