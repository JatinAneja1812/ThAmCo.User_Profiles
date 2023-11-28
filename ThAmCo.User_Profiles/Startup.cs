using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ThAmCo.User_Profiles.Automapper;
using ThAmCo.User_Profiles.DatabaseContext;
using ThAmCo.User_Profiles.Repositories.Repository.Classes;
using ThAmCo.User_Profiles.Repositories.Repository.Interfaces;
using ThAmCo.User_Profiles.Services.Service.Classes;
using ThAmCo.User_Profiles.Services.Service.Interfaces;
using AutoMapper;
using ThAmCo.User_Profiles.Utility;

namespace ThAmCo.User_Profiles
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    }
                );
            });

            // Configure JWT authentication.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = _configuration["Jwt:Authority"];
                options.Audience = _configuration["Jwt:Audience"];
            });


            // Configure the database context
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ConnectionString");
            services.AddDbContext<ProfilesContext>(options =>
                options.UseSqlServer(connectionString));

            // Add controllers
            services.AddControllers();

            services.AddAutoMapper(cfg => cfg.AddProfile<UserDataMappingProfile>());
            // Add services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IGuidUtility, GuidUtility>();

            // Add API endpoint exploration and Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Add the file provider
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                // EnsureCreated in development
                using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ProfilesContext>();
                dbContext.Database.EnsureCreated();

                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                // Apply migrations in production
                using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ProfilesContext>();
                dbContext.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();

            //JWT Bearer authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
