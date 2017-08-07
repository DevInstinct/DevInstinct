using DevInstinct.ErrorHandling;

namespace DevInstinct.Patterns.TierPattern
{
    public static class TierPatternExtensions
    {
        public static DomainErrorException ToDomainErrorException(this ValidationError error)
        {
            return new DomainErrorException(error);
        }

        public static RepositoryErrorException ToRepositoryErrorException(this ValidationError error)
        {
            return new RepositoryErrorException(error);
        }
    }
}
