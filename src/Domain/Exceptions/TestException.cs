namespace Domain.Exceptions;

public class TestException : DomainException
{
    private const string Error = "This is a test exception";

    public TestException() : base(Error)
    {
    }
}