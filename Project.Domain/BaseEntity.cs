namespace Project.Domain;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
