using Microsoft.Owin.Hosting;
using System;

namespace Server
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(ServerConstants.ServerUrl))
            {
                Console.WriteLine("Server running at {0} ... ", ServerConstants.ServerUrl);
                Console.ReadLine();
            }
        }
    }
}
