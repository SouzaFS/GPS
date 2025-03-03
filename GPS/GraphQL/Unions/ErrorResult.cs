namespace GPS.GraphQL.Unions{

    public class Result : IGraphQLResult{

        public string Message { get; set; }
        public string StatusCode { get; set; }

        public Result(string message, string code){
            Message = message;
            StatusCode = code;
        }

    }
}