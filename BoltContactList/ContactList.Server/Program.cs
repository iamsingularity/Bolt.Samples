using Microsoft.AspNet.Hosting;

namespace ContactList.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
