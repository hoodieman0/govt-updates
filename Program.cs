namespace GOV;

class Program
{
    static async Task Main(string[] args)
    {
        Driver driver = new Driver();
        await driver.Start();
    }
}