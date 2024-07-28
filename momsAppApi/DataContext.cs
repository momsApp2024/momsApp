using Microsoft.EntityFrameworkCore;
using momsAppApi.Dal;
using System.Collections.Generic;

using System.Reflection.Emit;

namespace momsAppApi
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<PersonalInformation> PersonalInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<City>()
        //        .HasMany(c => c.Addresses)
        //        .WithOne(a => a.City)
        //        .HasForeignKey(a => a.CityId)
        //        .OnDelete(DeleteBehavior.Restrict);  // Adjust delete behavior

        //    modelBuilder.Entity<City>()
        //        .HasMany(c => c.Districts)
        //        .WithOne(d => d.City)
        //        .HasForeignKey(d => d.CityId)
        //        .OnDelete(DeleteBehavior.Restrict);  // Adjust delete behavior

        //    modelBuilder.Entity<District>()
        //        .HasMany(d => d.Addresses)
        //        .WithOne(a => a.District)
        //        .HasForeignKey(a => a.DistrictId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<PersonalInformation>()
        //        .HasOne(pi => pi.Address)
        //        .WithMany(a => a.PersonalInformations)  // Assuming Address has a collection of PersonalInformations
        //        .HasForeignKey(pi => pi.AddressId)
        //        .OnDelete(DeleteBehavior.Restrict);  // Adjust delete behavior if neede
        //}
        {
            modelBuilder.Entity<City>()
                .HasMany(c => c.Addresses)
                .WithOne(a => a.City)
                .HasForeignKey(a => a.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<District>()
                .HasMany(d => d.Cities)
                .WithOne(c => c.District)
                .HasForeignKey(c => c.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<District>()
                .HasMany(d => d.Addresses)
                .WithOne(a => a.District)
                .HasForeignKey(a => a.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonalInformation>()
                .HasOne(pi => pi.Address)
                .WithMany(a => a.PersonalInformations)
                .HasForeignKey(pi => pi.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

