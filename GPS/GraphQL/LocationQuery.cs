using GPS.GraphQL.Interfaces;
using GPS.Models;
using GPS.Repositories.Interfaces;
using MongoDB.Driver.Linq;

namespace GPS.GraphQL{

    [ObjectType("Query")]
    public class LocationQuery : ILocationQuery{

        private readonly IBaseRepository<LocationModel> _baseRepository;

        public LocationQuery(IBaseRepository<LocationModel> baseRepository){
            _baseRepository = baseRepository;
        }
        
        public async Task<List<LocationModel>> GetLocations(){
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

        public async Task<LocationModel> GetLocationById(string id){
            
            try{
                var location = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();
                if (location != null)
                {
                    return location;
                }

                throw new Exception($"Location {id} not found.");
            }
            catch(Exception e){
                Console.WriteLine($"Error: {e.Message}");
                throw new Exception("Error getting location");
            }
        }   
    }
}