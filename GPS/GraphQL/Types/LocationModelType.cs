using GPS.Models;

namespace GPS.GraphQL.Types{
    public class LocationModelType : ObjectType<LocationModel>
    {
        protected override void Configure(IObjectTypeDescriptor<LocationModel> descriptor)
        {
            descriptor.Field(a => a.Id);
            descriptor.Field(a => a.Latitude);
            descriptor.Field(a => a.Longitude);
        }
    }
}