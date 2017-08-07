using System.Collections.Generic;
using System.Linq;

namespace DevInstinct.ErrorHandling
{
    // http://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/
    public class ValidationError : ApplicationError
    {
        public ValidationError(IEnumerable<PropertyValidationError> errors)
            : base("validation_failed", "Property Validation Failed")
        {
            Errors = errors.ToList();
        }

        public List<PropertyValidationError> Errors { get; }
    }
}
