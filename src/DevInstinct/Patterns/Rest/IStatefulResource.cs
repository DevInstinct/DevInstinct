namespace DevInstinct.Patterns.Rest
{
    public interface IStatefulResource
    {
        string[] ResourceStates { get; set; }
    }
}
