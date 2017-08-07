namespace DevInstinct.Patterns.Rest
{
    public interface ISecuredResource
    {
        string[] UserClaims { get; set; }
    }
}
