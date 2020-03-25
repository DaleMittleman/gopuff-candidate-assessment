namespace Microservice.CartManager
{
    using Microservice.CartManager.Repositories;
    using Microservice.CartManager.Utilities;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// Contains all custom startup logic and builds the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration container.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration container.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the DI container by adding service implementations and factory methods.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                });

            // Add swagger documentation configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {
                        Title = "Cart Managmeent API",
                        Description = "REST API for Cart Management developed for goPuff candidate evaluation",
                        Version = "v1",
                        Contact = new OpenApiContact()
                        {
                            Email = "dalemittleman@gmail.com",
                            Name = "Dale Mittleman",
                        },
                    });
            });

            // Add Redis configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "127.0.0.1";
            });

            // Add collection repositories
            services.AddSingleton<IProductRepository, ProductRepository>();
        }

        /// <summary>
        /// Configures the application middleware pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
