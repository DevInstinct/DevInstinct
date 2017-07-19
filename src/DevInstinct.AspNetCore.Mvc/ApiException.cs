using Microsoft.AspNetCore.Http;

namespace DevInstinct.AspNetCore.Mvc
{
    public class ApiException : ApplicationErrorException
    {
        public ApiException(ApplicationError error, int statusCode = StatusCodes.Status400BadRequest) : base(error)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
