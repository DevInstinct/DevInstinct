namespace DevInstinct
{
    // http://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/
    public class FieldValidationError : ApplicationError
    {
        public FieldValidationError(string field, string message)
            : base("field_validation_failed", message)
        {
            Field = field == string.Empty ? null : field;
        }

        public string Field { get; }
    }
}
