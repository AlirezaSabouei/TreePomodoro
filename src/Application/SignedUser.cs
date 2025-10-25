namespace Application;

public class SignedUser
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public int TreeGrowthTimeInSeconds { get; set; } = 1500;
}