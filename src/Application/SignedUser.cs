namespace Application;

public class SignedUser
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TreeGrowthTimeInSeconds { get; set; } = 20;
}