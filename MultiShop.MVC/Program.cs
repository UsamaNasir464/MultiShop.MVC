using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MultiShop.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //Wrote webhost builder instead of hostbuilder
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
     WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>();
    }
}
