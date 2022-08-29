using Citappuls.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Citappuls.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Speciality> Specialties { get; set; }
        public DbSet<State> States { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalDoctor> HospitalDoctors { get; set; }
        public DbSet<HospitalSpeciality> HospitalSpecialities { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<SpecialityDoctor> SpecialityDoctors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasIndex("Name", "StateId").IsUnique();
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("Name", "CountryId").IsUnique();

            modelBuilder.Entity<Doctor>().HasIndex("Name", "LastName").IsUnique();
            modelBuilder.Entity<Speciality>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Hospital>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<SpecialityDoctor>().HasIndex("DoctorId", "SpecialityId").IsUnique();
            modelBuilder.Entity<HospitalSpeciality>().HasIndex("HospitalId", "SpecialityId").IsUnique();
            modelBuilder.Entity<HospitalDoctor>().HasIndex("HospitalId", "DoctorId").IsUnique();

            
        }
            
    }
}
