using Microsoft.AspNet.Hosting;

namespace MemoService.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
