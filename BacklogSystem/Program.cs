using System;
using Microsoft.AspNetCore.Hosting;

namespace BacklogSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:9000")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
