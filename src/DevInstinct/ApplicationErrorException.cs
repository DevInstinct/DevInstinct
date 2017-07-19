using System;

namespace DevInstinct
{
    public class ApplicationErrorException : ApplicationException
    {
        public ApplicationErrorException(ApplicationError error)
        {
            Error = error;
        }
        public ApplicationError Error { get; }
    }
}
