using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using DomainLayer;
using ServiceLayer;
using DomainLayer.Models;
using MailKit;
using ServiceLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repository;
using ServiceLayer.Interfaces.ICommonService;
using RepositoryLayer.Repository.CommonRepository;
using ServiceLayer.Services.CommonService;
using RepositoryLayer.Interfaces.ICommonRepository;
using ServiceLayer.Services.Encryption;
using ServiceLayer.Interfaces.IEncription;
using ServiceLayer.Services.Email;
using ServiceLayer.Interfaces.IZoom;
using ServiceLayer.Services.Zoom;
using ServiceLayer.Interfaces.IAppointmentService;
using ServiceLayer.Services.AppointmentService;
using RepositoryLayer.Repository.AppointmentRepository;
using RepositoryLayer.Interfaces.IAppointmentRepository;
using ServiceLayer.Interfaces.INurseDashboard;
using RepositoryLayer.Interfaces.INurseDashRepository;
using ServiceLayer.Services.NurseDashBoardServices;
using RepositoryLayer.Repository.NurseDashRepository;
using RepositoryLayer.Repository.InboxRepository;
using ServiceLayer.Services.InboxService;
using ServiceLayer.Interfaces.IInboxService;
using RepositoryLayer.Interfaces.IInboxRepository;
using ServiceLayer.Interfaces.IVisitDetails;
using ServiceLayer.Services.VisitdetailsService;
using RepositoryLayer.Repository.VisitdetailsRepository;
using RepositoryLayer.Interfaces.IVisitdetailsRepository;
using ServiceLayer.Interfaces.IMasterService;
using ServiceLayer.Services.MasterService;
using RepositoryLayer.Repository.MasterRepository;
using RepositoryLayer.Interfaces.IMasterRepository;

namespace LoginAPI
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
            //Inject AppSettings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEncryption, Encryption>();
            services.AddTransient<IMessageService, ServiceLayer.MessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped<IInMemoryCache, InMemoryCache>();


            // Appointmenet
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IZoom, Zoom>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // CommonAPI
            services.AddScoped<INurseService, NurseDashService>();
            services.AddScoped<INurseRepository, NurseDashRepository>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IEncryption, Encryption>();

            // InboxAPI
            services.AddScoped<INotesService, NotesService>();
            services.AddScoped<INotesRepository, NotesRepository>();

            // VisitDetails
            services.AddScoped<IVisitService, PatientvisitService>();
            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<IPatientvisitdetailsService, PatientvisitdetailsService>();
            services.AddScoped<IPatientvisitdetailRepository, PatientvisitdetailsRepository>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IMasterRepository, MasterRepository>();


            services.AddMemoryCache();

            services.AddControllers();
            services.AddSession(options =>
            {
                options.Cookie.Name = "HMS";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CosmosMW", Version = "v1" });
            });

            //DB Connection
            string connection = Configuration.GetConnectionString("LoginConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection,
                                                         b => b.MigrationsAssembly("HospitalAPI"))
            );

            //Identity
            services.AddDefaultIdentity<ApplicationUser>()
             .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            services.AddControllers().AddNewtonsoftJson();

            //CORS
            services.AddCors();

            //JWT Authentication

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
            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CosmosMW v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            //CORS
            //app.UseCors(builder =>
            //builder.WithOrigins(Configuration["Jwt:Client_Url"].ToString())
            //.AllowAnyHeader()
            //.AllowAnyMethod()
            //);



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
