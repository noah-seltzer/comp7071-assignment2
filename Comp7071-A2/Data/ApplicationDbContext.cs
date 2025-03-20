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

    /************************************************************************************
     * Manage Housing
    *************************************************************************************/
    public DbSet<Renter> Renters { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Suite> Suites { get; set; }
    public DbSet<Locker> Lockers { get; set; }
    public DbSet<ParkingSpot> ParkingSpots { get; set; }
    public DbSet<HousingGroup> HousingGroups { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Contact> Contacts { get; set; } = default!;
    public DbSet<Application> Applications { get; set; } = default!;
    public DbSet<ApplicationReference> ApplicationReferences { get; set; } = default!;
    
    public DbSet<AssetDamage> AssetDamages { get; set; }
    
    public DbSet<DamageImage> DamageImages { get; set; }

    //***********************************************************************************


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
        var housingAdminRole = new IdentityRole("HousingAdmin") { Id = "b5f0c6a4-45d7-4e18-94df-bc3b0e69c456", NormalizedName = "HOUSINGADMIN" };
        var userRole = new IdentityRole("User") { Id = "6a4d3c5f-95df-4e18-bc3b-0e69c457c6a4", NormalizedName = "USER" };
        var renterRole = new IdentityRole("Renter") { Id = "6a4d3c5f-95df-4e18-bc3b-0e69c457c234", NormalizedName = "RENTER" };
        modelBuilder.Entity<IdentityRole>().HasData(housingAdminRole, userRole, renterRole);

        /********************************************************************
        * Manage Housing
        *********************************************************************/
        modelBuilder.Entity<Asset>()
            .HasDiscriminator<string>("AssetType")
            .HasValue<Locker>("Locker")
            .HasValue<ParkingSpot>("ParkingSpot")
            .HasValue<Suite>("Suite");

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

        modelBuilder.Entity<Renter>()
            .HasMany(r => r.Assets)
            .WithOne(a => a.Renter)
            .HasForeignKey(a => a.RenterID)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Asset>()
            .HasMany(a => a.AssetDamages)
            .WithOne(ad => ad.Asset)
            .HasForeignKey(ad => ad.AssetID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AssetDamage>()
            .HasMany(ad => ad.DamageImages)
            .WithOne(d => d.AssetDamage)
            .HasForeignKey(d => d.AssetDamageID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Renter>()
            .HasMany(r => r.AssetDamages)
            .WithOne(ad => ad.Renter)
            .HasForeignKey(ad => ad.RenterID)
            .IsRequired();
        
        modelBuilder.Entity<Renter>()
            .HasMany(r => r.AssetInvoices)
            .WithOne(a => a.Renter)
            .HasForeignKey(a => a.RenterId)
            .IsRequired();
        
        modelBuilder.Entity<Asset>()
            .HasMany(a => a.AssetInvoices)
            .WithOne(ai => ai.Asset)
            .HasForeignKey(ai => ai.AssetId)
            .IsRequired();

        //********************************************************************


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

public DbSet<Comp7071_A2.Areas.Housing.Models.AssetInvoice> AssetInvoice { get; set; } = default!;


}
