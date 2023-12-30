using System.Collections;
using System.Collections.Generic;

namespace Talabat.API.Errors
{
    public class ApivalidationErrorsResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApivalidationErrorsResponse() : base(400)
        {
            
        }
    }
}
