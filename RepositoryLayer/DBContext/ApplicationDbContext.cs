using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;
using DomainLayer.EntityModels;
using DomainLayer.EntityModels.DashBoard;
using DomainLayer.EntityModels.Master;
using DomainLayer.EntityModels.Procedures;
using DomainLayer.Models;
using DomainLayer.Models.Master;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace RepositoryLayer
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Allergy> Allergy { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Drug> Drug { get; set; }
        public DbSet<Procedure> Procedure { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<RoleMaster> RoleMaster { get; set; }
        public DbSet<PatientDetails> PatientDetails { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<NoteData> NoteData { get; set; }

        public DbSet<Appointments> Appointments { get; set; }


        //Dashboard
        public DbSet<AdminDashBoard> AdminDashboard { get; set; }

        //public DbSet<UserInfo> UserInfos { get; set; }

      //  public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<ResultStatus> Result { get; set; }
        public DbSet<BarChartDetails> BarChartDetails { get; set; }
        public DbSet<PatientDemographicDetails> PatientDemographicDetails { get; set; }

        public DbSet<PatientVisitDetails> PatientVisitDetails { get; set; }
        public DbSet<PatientRelativeDetails> PatientRelativeDetails { get; set; }
        //public DbSet<EmployeeAvailability> EmployeeAvailability { get; set; }

        //public DbSet<Logger> Logger { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Education> Education { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<Designation> Designation { get; set; }


        public DbSet<Subscription> Subscription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<UserDetails>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

            modelBuilder.Entity<UserDetails>()
            .HasOne<PatientDetails>(s => s.PatientDetails)
            .WithOne(ad => ad.UserDetails)
            .HasForeignKey<PatientDetails>(x => x.UserId);

            modelBuilder.Entity<UserDetails>()
           .HasOne<EmployeeDetails>(s => s.EmployeeDetails)
           .WithOne(ad => ad.UserDetails)
           .HasForeignKey<EmployeeDetails>(x => x.UserId);

            modelBuilder.Entity<UserDetails>()
            .HasOne<RoleMaster>(s => s.RoleMaster)
            .WithMany(ad => ad.lstUserDetails)
            .HasForeignKey(x => x.RoleId);

            modelBuilder
           .Entity<RoleMaster>().Property(x => x.Id)
           .HasDefaultValueSql("newid()");

            modelBuilder
            .Entity<EmployeeDetails>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

            modelBuilder
           .Entity<PatientDetails>().Property(x => x.Id)
           .HasDefaultValueSql("newid()");

            modelBuilder
           .Entity<Notes>().Property(x => x.Id)
           .HasDefaultValueSql("newid()");

            modelBuilder.Entity<Notes>()
           .HasOne(s => s.SenderEmployeeDetails)
           .WithMany(g => g.lstSentNotes)
           .HasForeignKey(s => s.SenderEmployeeId)
           .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Notes>()
            .HasOne(s => s.RecieverEmployeeDetails)
            .WithMany(g => g.lstRecieverNotes)
            .HasForeignKey(s => s.RecieverEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
            .Entity<Drug>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

            modelBuilder
            .Entity<Allergy>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

            modelBuilder
            .Entity<Diagnosis>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

            modelBuilder
            .Entity<Procedure>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");
            modelBuilder
.Entity<BarChartDetails>().Property(x => x.Id);
            modelBuilder
            .Entity<PatientDemographicDetails>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

            modelBuilder
           .Entity<Appointments>().Property(x => x.Id)
           .HasDefaultValueSql("newid()");

           // modelBuilder.Entity<Appointments>()
           //.HasOne(s => s.PatientDetails)
           //.WithMany(g => g.lstAppointments)
           //.HasForeignKey(s => s.PatientId)
           //.OnDelete(DeleteBehavior.Restrict);


           // modelBuilder.Entity<Appointments>()
           //.HasOne(s => s.PhysicianEmployee)
           //.WithMany(g => g.lstAppointments)
           //.HasForeignKey(s => s.PatientId)
           //.OnDelete(DeleteBehavior.Restrict);


           // modelBuilder.Entity<Appointments>()
           //.HasOne(s => s.PatientDetails)
           //.WithMany(g => g.lstAppointments)
           //.HasForeignKey(s => s.PatientId)
           //.OnDelete(DeleteBehavior.Restrict);


            modelBuilder
            .Entity<PatientDemographicDetails>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

            modelBuilder.Entity<PatientDemographicDetails>()
           .HasOne(s => s.PatientRelativeDetails)
           .WithOne(g => g.PatientDemographicDetails)
           .HasForeignKey<PatientRelativeDetails>(s => s.PatientDemographicsId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
            .Entity<PatientRelativeDetails>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");


            //Skip Tables
            modelBuilder.Ignore<AdminDashBoard>();
            //modelBuilder.Ignore<UserInfo>();


            modelBuilder
            .Entity<Education>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

            modelBuilder
            .Entity<Department>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

             modelBuilder
            .Entity<Designation>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");


            modelBuilder
            .Entity<Subscription>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseSqlServer(
            //    "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=CosmosDB");
        }
    }
}
