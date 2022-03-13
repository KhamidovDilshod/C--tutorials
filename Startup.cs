using C__tutorials.Interface;
using C__tutorials.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;

namespace Tutorials
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
            .AddNewtonsoftJson(jsonOptions =>
        {
            jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(x =>
            x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}