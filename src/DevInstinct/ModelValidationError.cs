using System.Collections.Generic;
using System.Linq;

namespace DevInstinct
{
    // http://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/
    public class ModelValidationError : ApplicationError
    {
        public ModelValidationError(IEnumerable<FieldValidationError> errors)
            : base("model_validation_failed", "Field Validation Failed")
        {
            Errors = errors.ToList();
        }

        public List<FieldValidationError> Errors { get; }
    }
}
