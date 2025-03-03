using GPS.Models;

namespace GPS.GraphQL.Unions{

    public class LocationListResult : IGraphQLResult{
        public List<LocationModel> Locations { get; set; }

        public LocationListResult(List<LocationModel> locations){
            Locations = locations;
        }
    }

    public class LocationResult : IGraphQLResult{
        public LocationModel Location { get; set; }
        public LocationResult(LocationModel location){
            Location = location;
        }
    }
}