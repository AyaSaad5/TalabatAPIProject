using System;

namespace Talabat.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStautsCode(statusCode);
        }

        private string GetDefaultMessageForStautsCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request",
                401 => "Un Authorized",
                404 => "Not Found",
                500 => "Errors are the path to the dark side, Errors lead to anger, Anger leads to hate, Hate leads to shift career"
            };
        }
    }
}
