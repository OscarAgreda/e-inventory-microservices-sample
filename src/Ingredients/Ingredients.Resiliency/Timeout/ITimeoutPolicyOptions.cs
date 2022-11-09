namespace Ingredients.Resiliency.Timeout;

public interface ITimeoutPolicyOptions
{
    public int TimeOutDuration { get; set; }
}
