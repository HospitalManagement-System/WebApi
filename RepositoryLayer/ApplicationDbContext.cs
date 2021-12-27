using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;
using DomainLayer.EntityMapper;
using DomainLayer.EntityMapper.MasterMapper;
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
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Allergy> Allergy { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Drug> Drug { get; set; }
        public DbSet<Procedure> Procedure { get; set; }
        public DbSet<RoleMaster> RoleMaster { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<PatientDetails> PatientDetails { get; set; }
        public DbSet<PatientDemographicDetails> PatientDemographicDetails { get; set; }
        public DbSet<PatientRelativeDetails> PatientRelativeDetails { get; set; }
        public DbSet<PatientVisitDetails> PatientVisitDetails { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<UserDetails>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new UserDetailsMapper());

            modelBuilder
           .Entity<RoleMaster>().Property(x => x.Id)
           .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new RoleMasterMapper());

            modelBuilder
            .Entity<EmployeeDetails>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new EmployeeDetailsMapper());

            modelBuilder
            .Entity<Notes>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new NotesMapper());

            modelBuilder
           .Entity<Appointments>().Property(x => x.Id)
           .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new AppointmentsMapper());

            modelBuilder
           .Entity<PatientDemographicDetails>().Property(x => x.Id)
           .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new PatientDemographicDetailsMapper());

            modelBuilder
           .Entity<PatientDetails>().Property(x => x.Id)
           .HasDefaultValueSql("newid()");
           // modelBuilder.ApplyConfiguration(new PatientDetailsMapper());

            modelBuilder
            .Entity<PatientRelativeDetails>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new PatientRelativeDetailsMapper());

            modelBuilder
            .Entity<Drug>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new DrugMapper());

            modelBuilder
            .Entity<Allergy>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new AllergyMapper());

            modelBuilder
            .Entity<Diagnosis>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new DiagnosisMapper());

            modelBuilder
            .Entity<Procedure>().Property(x => x.Id)
            .HasDefaultValueSql("newid()");
            //modelBuilder.ApplyConfiguration(new ProcedureMapper());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //.UseNpgsql(
            //"Host=localhost; Port=5432; Database=CosmosDB; User Id=postgres; password=root");
            optionsBuilder
                .UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=CosmosDB");
        }
    }
}
