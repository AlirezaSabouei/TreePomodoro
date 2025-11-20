using Domain.Entities.Glasses;
using NUnit.Framework;

namespace Domain.UnitTests.Entities.Glasses;

public class GlassTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Glass glass = new();
        glass.ClearDomainEvents();
        Assert.Pass();
    }
}
