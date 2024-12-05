namespace Solutions;

public interface ISolution
{
    Task<object> ExecuteAsync();
}