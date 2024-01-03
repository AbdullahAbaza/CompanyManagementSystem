using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Company.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //var hostBuilder = CreateHostBuilder(args).Build();

            // Data Seeding
            // Update Database
            //hostBuilder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
