namespace Domain;

public class DomainException : Exception
{
    protected DomainException(string error) : base(error) 
    {
    }
}