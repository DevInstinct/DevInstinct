using DevInstinct.ErrorHandling;

namespace DevInstinct.Patterns.TierPattern
{
    public class RepositoryErrorException : SystemErrorException
    {
        public RepositoryErrorException(ApplicationError error) : base(error)
        {
        }
    }
}
