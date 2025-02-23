using GPS.DTOs;

namespace GPS.GraphQL.Types{
    public class UserDTOType : ObjectType<UserDTO>
    {
        protected override void Configure(IObjectTypeDescriptor<UserDTO> descriptor)
        {
            descriptor.Field(a => a.Username);
            descriptor.Field(a => a.Email);
            descriptor.Field(a => a.FederalID);
            descriptor.Field(a => a.FirstName);
            descriptor.Field(a => a.LastName);
        }
    }
}