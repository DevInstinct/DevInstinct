namespace DevInstinct
{
    public class ApplicationError
    {
        public ApplicationError(string id, string message)
        {
            Id = id;
            Message = message;
        }

        public string Id { get; set; }

        public string Message { get; set; }

        public string Detail { get; set; }
    }
}
