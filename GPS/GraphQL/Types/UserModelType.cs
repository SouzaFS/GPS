using GPS.Models;

namespace GPS.GraphQL.Types{
    public class UserModelType : ObjectType<UserModel>
    {
        protected override void Configure(IObjectTypeDescriptor<UserModel> descriptor)
        {
            descriptor.Field(a => a.Id);
            descriptor.Field(a => a.Username);
            descriptor.Field(a => a.FederalID);
            descriptor.Field(a => a.FirstName);
            descriptor.Field(a => a.LastName);
        }
    }
}