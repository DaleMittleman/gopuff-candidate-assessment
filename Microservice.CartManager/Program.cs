namespace Microservice.CartManager
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Provides the .dll entrypoint.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The execution entrypoint.
        /// </summary>
        /// <param name="args">The standard args array.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates a host builder.
        /// </summary>
        /// <param name="args">The standard args array.</param>
        /// <returns>A <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
