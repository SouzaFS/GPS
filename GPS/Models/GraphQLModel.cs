using GPS.GraphQL.Controllers.Interfaces;

namespace GPS.Models{

    public class GraphQL<GenericType> : IGraphQLResult where GenericType : class{
        public GenericType? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public required string StatusCode { get; set; }
        public required bool Success { get; set; }

        public static GraphQL<GenericType> Problem(string message){
            return new GraphQL<GenericType>(){
                Data = null,
                Message = message,
                Success = false,
                StatusCode = "500"
            };
        }

        public static GraphQL<GenericType> Ok(GenericType data){
            return new GraphQL<GenericType>(){
                Data = data,
                Success = true,
                StatusCode = "200"
            };
        }

        public static GraphQL<GenericType> NotFound(){
            return new GraphQL<GenericType>(){
                Data = null,
                Message = "Not Found",
                Success = false,
                StatusCode = "404"
            };
        }

        public static GraphQL<GenericType> Created(GenericType data){
            return new GraphQL<GenericType>(){
                Data = data,
                Message = "Created",
                Success = true,
                StatusCode = "201"
            };
        }

        public static GraphQL<GenericType> BadRequest(){
            return new GraphQL<GenericType>(){
                Data = null,
                Message = "Bad Request",
                Success = false,
                StatusCode = "400"
            };
        }

        public static GraphQL<GenericType> NoContent(){
            return new GraphQL<GenericType>(){
                Data = null,
                Message = "No Content",
                Success = true,
                StatusCode = "204"
            };
        }

        public static GraphQL<GenericType> UnprocessableEntity(){
            return new GraphQL<GenericType>(){
                Data = null,
                Message = "Unprocessable Entity",
                Success = false,
                StatusCode = "422"
            };
        }
    }
}