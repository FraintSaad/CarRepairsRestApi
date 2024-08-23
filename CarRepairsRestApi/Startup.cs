using CarRepairsRestApi.Database;
using CarRepairsRestApi.Models;
using CarRepairsRestApi.Repositories.Implimentations;
using CarRepairsRestApi.Repositories.Interfaces;
using CarRepairsRestApi.Services.Implimentations;
using CarRepairsRestApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CarRepairsRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddMvc();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));

            services.AddTransient<IRepairService, RepairService>();
            services.AddTransient<IBaseRepository<Document>, BaseRepository<Document>>();
            services.AddTransient<IBaseRepository<Car>, BaseRepository<Car>>();
            services.AddTransient<IBaseRepository<Worker>, BaseRepository<Worker>>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CarRepairsRestApi",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRepairsRestApi v1");
            });
        }

        // The Main method that serves as the entry point for the application.
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // A helper method to create the IHostBuilder for the application.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}