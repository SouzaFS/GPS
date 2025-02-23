using GPS.DTOs;
using GPS.Models;

namespace GPS.Mappers{

    public static class LocationMapper{
        public static LocationModel FromDTOToModel(LocationDTO locationDTO){
            
            return new LocationModel{
                Latitude = locationDTO.Latitude,
                Longitude = locationDTO.Longitude,
            };
        }

        public static LocationDTO FromModelToDTO(LocationModel locationModel){
            
            return new LocationDTO{
                Latitude = locationModel.Latitude,
                Longitude = locationModel.Longitude,
            };
        }
    }
}