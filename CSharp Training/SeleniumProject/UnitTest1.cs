

namespace SeleniumProject;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        TestContext.Progress.WriteLine("we are in setup");
    }

    [Test]
    public void Test1()
    {
        TestContext.Progress.WriteLine("we are in test1");
        Assert.Pass();
    }
}
