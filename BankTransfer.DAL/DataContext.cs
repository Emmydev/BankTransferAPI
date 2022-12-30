using BankTransfer.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.DAL
{
    public class DataContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        public DataContext()
        {

        }
        public DbSet<Provider> Provider { get; set; }
        public  DbSet<Organization> Organization { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            builder.Entity<ApplicationRole>().ToTable("ApplicationRole");
            builder.Entity<ApplicationUserClaim>().ToTable("ApplicationUserClaim");
            builder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRole");
            builder.Entity<ApplicationUserLogin>().ToTable("ApplicationUserLogin");
            builder.Entity<ApplicationRoleClaim>().ToTable("ApplicationRoleClaim");
            builder.Entity<ApplicationUserToken>().ToTable("ApplicationUserToken");

            builder.Entity<Organization>()
            .ToTable("Organization");
            builder.Entity<Organization>()
            .HasData(
                new Organization
                {
                    ClientId = 1,
                    Name = "Index",
                    ClientSecret = "YT33#@*(^%@@!#EWADE#",
                    Deleted = false,
                    Email = "info@innovectives.com",
                    Address = "14, Yeye Olofin Street, Lekki Phase 1, Lagos, Nigeria",
                    CreatedOn = DateTime.Now
                }
            );

            builder.Entity<Provider>()
            .ToTable("Provider");
            builder.Entity<Provider>()
            .HasData(
                new Provider
                {
                    ProviderId = 1,
                    Name = "Paystack",
                    Deleted = false,
                    CreatedOn = DateTime.Now
                },
                new Provider
                {
                    ProviderId = 2,
                    Name = "Flutterwave",
                    Deleted = false,
                    CreatedOn = DateTime.Now
                }
            );
        }
    }
}
