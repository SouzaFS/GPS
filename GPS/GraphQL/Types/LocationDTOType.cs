using GPS.DTOs;

namespace GPS.GraphQL.Types{
    public class LocationDTOType : ObjectType<LocationDTO>
    {
        protected override void Configure(IObjectTypeDescriptor<LocationDTO> descriptor)
        {
            descriptor.Field(a => a.Latitude);
            descriptor.Field(a => a.Longitude);
        }
    }
}