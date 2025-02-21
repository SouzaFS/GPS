using GPS.GraphQL.Interfaces;
using GPS.Models;
using GPS.Repositories.Interfaces;
using MongoDB.Driver.Linq;

namespace GPS.GraphQL{

    [QueryType]
    public class LocationQuery : ILocationQuery{

        private readonly IBaseRepository<LocationModel> _baseRepository;

        public LocationQuery(IBaseRepository<LocationModel> baseRepository){
            _baseRepository = baseRepository;
        }
        
        [UseFiltering]
        [UseProjection]
        public async Task<List<LocationModel>> GetAllLocations(){
            try{
                var locations = await _baseRepository.GetAll().ToListAsync();
                if (locations.Count > 0)
                {
                    return locations;
                }

                throw new Exception("No locations found.");
            }
            catch(Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error getting locations");
            }
        }

        [UseFiltering]
        [UseProjection]
        public async Task<LocationModel> GetLocationByUserId(string userId){
            
            try{
                var location = await _baseRepository.GetByWhere(a => a.UserId == userId).FirstOrDefaultAsync();
                if (location != null)
                {
                    return location;
                }

                throw new Exception($"Location associated to userId {userId} not found.");
            }
            catch(Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error getting location");
            }
        }   
    }
}