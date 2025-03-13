using Comp7071_A2.Areas.Housing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Areas.ManageCare.Models;
using Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models;

namespace Comp7071_A2.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    /**
     * Manage Housing
     */
    public DbSet<Renter> Renters { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Suite> Suites { get; set; }
    public DbSet<Locker> Lockers { get; set; }
    public DbSet<ParkingSpot> ParkingSpots { get; set; }
    public DbSet<HousingGroup> HousingGroups { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Contact> Contact { get; set; } = default!;
    public DbSet<Application> Application { get; set; } = default!;
    public DbSet<ApplicationReference> ApplicationReference { get; set; } = default!;


    public DbSet<Employee> Employees { get; set; }
    public DbSet<Certification> Certifications { get; set; }
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
    public DbSet<HRManager> HRManagers { get; set; }
    public DbSet<HRSchedule> HRSchedules { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    public DbSet<PayPeriod> PayPeriods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       
        modelBuilder.Entity<HREmployee>().ToTable("HREmployees");
        
        modelBuilder.Entity<HRManager>()
            .ToTable("HRManagers");

        // Create Roles
        var housingAdminRole = new IdentityRole("HousingAdmin") { Id = "b5f0c6a4-45d7-4e18-94df-bc3b0e69c456" };
        var userRole = new IdentityRole("User") { Id = "6a4d3c5f-95df-4e18-bc3b-0e69c457c6a4" };
        modelBuilder.Entity<IdentityRole>().HasData(housingAdminRole, userRole);

        modelBuilder.Entity<Renter>()
            .HasOne(r => r.Asset)
            .WithOne()
            .HasForeignKey<Renter>(r => r.AssetID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Renter>()
            .HasOne(r => r.Application)
            .WithOne()
            .HasForeignKey<Renter>(r => r.ApplicationID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Suite>()
            .HasOne(s => s.Asset)
            .WithOne()
            .HasForeignKey<Suite>(s => s.AssetID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Suite>()
            .HasOne(s => s.Locker)
            .WithOne(l => l.Suite)
            .HasForeignKey<Suite>(s => s.LockerID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Suite>()
            .HasOne(s => s.ParkingSpot)
            .WithOne(p => p.Suite)
            .HasForeignKey<Suite>(s => s.ParkingSpotID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Locker>()
            .HasOne(l => l.Asset)
            .WithOne()
            .HasForeignKey<Locker>(l => l.AssetID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ParkingSpot>()
            .HasOne(p => p.Asset)
            .WithOne()
            .HasForeignKey<ParkingSpot>(p => p.AssetID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.ParkingSpot)
            .WithOne(p => p.Vehicle)
            .HasForeignKey<Vehicle>(v => v.ParkingSpotID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApplicationReference>()
            .HasOne(ar => ar.Application)
            .WithMany(a => a.ApplicationReferences)
            .HasForeignKey(ar => ar.ApplicationID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ApplicationReference>()
            .HasOne(ar => ar.Contact)
            .WithMany(c => c.ApplicationReferences)
            .HasForeignKey(ar => ar.ContactID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Renter)
            .WithMany()
            .HasForeignKey(c => c.RenterID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasDiscriminator<string>("JobTitle")
            .HasValue<Employee>("Employee")
            .HasValue<Manager>("Manager");

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


}
