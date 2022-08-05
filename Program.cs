namespace ERMapping
{
  class Program
  {
    public static void Main()
    {
      MainAsync().Wait();
    }

    private static async Task MainAsync()
    {
      var res = await ERMapping.GenerateFiles();
    }
  }
}
