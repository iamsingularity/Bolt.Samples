using Microsoft.AspNet.Hosting;

namespace DistributedSession.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
