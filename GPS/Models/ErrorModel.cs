using GPS.GraphQL.Unions;

namespace GPS.Models{

    public class ErrorModel : IUserResult{

        public string Message { get; set; }
        public string Code { get; set; }
    }
}