using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevInstinct.AspNetCore.Mvc.ModelBinding
{
    // http://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/
    public class ModelStateValidationFailedResult : ObjectResult
    {
        public ModelStateValidationFailedResult(ModelStateDictionary modelState)
            : base(new ModelStateValidationError(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
