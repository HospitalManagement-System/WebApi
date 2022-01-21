using DomainLayer;
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

            //services.AddMvc().AddJsonOptions(options => {
            //    options.jsonSerializerSettings.MaxDepth = 64;  // or however deep you need
            //});
            services.AddControllers().AddNewtonsoftJson(options =>
           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddMvc().AddNewtonsoftJson(o =>
            //{
            //    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //});

            services.AddCors();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
