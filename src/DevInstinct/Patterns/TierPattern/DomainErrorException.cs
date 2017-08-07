using DevInstinct.ErrorHandling;

namespace DevInstinct.Patterns.TierPattern
{
    public class DomainErrorException : ApplicationErrorException
    {
        public DomainErrorException(ApplicationError error) : base(error)
        {
        }
    }
}
