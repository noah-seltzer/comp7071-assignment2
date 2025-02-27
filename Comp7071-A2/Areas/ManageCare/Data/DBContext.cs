using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Areas.ManageCare.Models;
using Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models;
using System.Reflection.Metadata;

namespace Comp7071_A2.Areas.ManageCare.Data
{
    public class CareManageMentDBContext : DbContext
    {
        public CareManageMentDBContext(DbContextOptions<CareManageMentDBContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasDiscriminator<string>("JobTitle")
                .HasValue<Employee>("Peasent")
                .HasValue<Manager>("Noble");


            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Lines)
                .WithOne(l => l.Invoice)
                .HasForeignKey(i => i.InvoiceId)
                .IsRequired();


            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Invoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Schedules)
                .WithMany(s => s.Customers)
                .UsingEntity(j => j.ToTable("CustomerSchedule"));

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Schedule)
                .WithMany(s => s.Employees)
                .UsingEntity(j => j.ToTable("EmployeeSchedule"));


            modelBuilder.Entity<Service>()
                .HasMany(s => s.Schedule)
                .WithOne(s => s.Service)
                .HasForeignKey(s => s.ServiceId)
                .IsRequired();


            modelBuilder.Entity<Certification>()
                .HasMany(c => c.Services)
                .WithMany(s => s.Certifications)
                .UsingEntity(j => j.ToTable("ServiceCertification"));
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        /**
         * Manage Human Resources
         */

        public DbSet<HREmployee> HREmployees { get; set; }
        public DbSet<HRSchedule> HRSchedules { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        public DbSet<PayPeriod> PayPeriods { get; set; }
    }
}
