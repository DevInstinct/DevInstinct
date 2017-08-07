using System;

namespace DevInstinct.ErrorHandling
{
    public class SystemErrorException : Exception
    {
        public SystemErrorException(ApplicationError error)
        {
            Error = error;
        }
        public ApplicationError Error { get; }
    }
}
