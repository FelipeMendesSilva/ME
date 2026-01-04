using System.Net;

namespace ME.SuperHero.Domain.Result
{
    public class  Result
    {
        public HttpStatusCode StatusCode { get; }
        public object? Data { get; }
        public string? ErrorMessage { get; }

        private Result(HttpStatusCode statusCode, object? data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        private Result(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
        
        public static Result Success(object data, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new Result(statusCode, data);

        public static Result Failure(string errorMessage, HttpStatusCode statusCode)
            => new Result(statusCode, errorMessage);

        public bool IsSuccess() => StatusCode == HttpStatusCode.OK || StatusCode == HttpStatusCode.Created;
    }
}
