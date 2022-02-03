using DomainLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Interfaces.IMasterRepository;
using RepositoryLayer.Interfaces.IVisitdetailsRepository;
using RepositoryLayer.Repository;
using RepositoryLayer.Repository.MasterRepository;
using RepositoryLayer.Repository.VisitdetailsRepository;
using ServiceLayer;
using ServiceLayer.Interfaces;
using ServiceLayer.Interfaces.IMasterService;
using ServiceLayer.Interfaces.IVisitDetails;
using ServiceLayer.Services.MasterService;
using ServiceLayer.Services.VisitdetailsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitDetailsAPI
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
            services.AddScoped<IVisitService, PatientvisitService>();
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<IPatientvisitdetailsService, PatientvisitdetailsService>();
            services.AddScoped<IPatientvisitdetailRepository, PatientvisitdetailsRepository>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VisitDetailsAPI", Version = "v1" });
            });
            string connection = Configuration.GetConnectionString("PatientConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection,
                                                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );
            

            //Identity
            services.AddDefaultIdentity<ApplicationUser>()
             .AddEntityFrameworkStores<ApplicationDbContext>();
          
            services.AddControllers().AddNewtonsoftJson(options =>
           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors();

            var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Key"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VisitDetailsAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
