using GPS.GraphQL.Interfaces;

namespace GPS.Models{

    public class GraphQLModel<GenericType> : IGraphQLResult where GenericType : class{
        public GenericType? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public required string Code { get; set; }
        public required bool Success { get; set; }

        public static GraphQLModel<GenericType> Problem(string message){
            return new GraphQLModel<GenericType>(){
                Data = null,
                Message = message,
                Success = false,
                Code = "500"
            };
        }

        public static GraphQLModel<GenericType> Ok(GenericType data){
            return new GraphQLModel<GenericType>(){
                Data = data,
                Success = true,
                Code = "200"
            };
        }

        public static GraphQLModel<GenericType> NotFound(){
            return new GraphQLModel<GenericType>(){
                Data = null,
                Message = "Not Found",
                Success = false,
                Code = "404"
            };
        }

        public static GraphQLModel<GenericType> Created(GenericType data){
            return new GraphQLModel<GenericType>(){
                Data = data,
                Message = "Created",
                Success = true,
                Code = "201"
            };
        }

        public static GraphQLModel<GenericType> BadRequest(){
            return new GraphQLModel<GenericType>(){
                Data = null,
                Message = "Bad Request",
                Success = false,
                Code = "400"
            };
        }

        public static GraphQLModel<GenericType> NoContent(){
            return new GraphQLModel<GenericType>(){
                Data = null,
                Message = "No Content",
                Success = true,
                Code = "204"
            };
        }

        public static GraphQLModel<GenericType> UnprocessableEntity(){
            return new GraphQLModel<GenericType>(){
                Data = null,
                Message = "Unprocessable Entity",
                Success = false,
                Code = "422"
            };
        }
    }
}