namespace DevInstinct.ErrorHandling
{
    public class ApplicationError
    {
        public ApplicationError(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public string ErrorCode { get; set; }

        public string Message { get; set; }

        public string Detail { get; set; }
    }
}
