namespace DevInstinct.ErrorHandling
{
    public class PropertyValidationError : ApplicationError
    {
        public PropertyValidationError(string propertyName, string message)
            : base("property_validation_failed", message)
        {
            PropertyName = propertyName == string.Empty ? null : propertyName;
        }

        public string PropertyName { get; }

        public ValidationError ToValidationError()
        {
            return new ValidationError(new[] { this });
        }
    }
}
