using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Comp7071_A2.Areas.Housing.Models;

namespace Comp7071_A2.Data
{
    public static class SeedData
    {
        public static async Task SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure the database is created and migrated
            await context.Database.MigrateAsync();

            if (context.Users.Any())
            {
                Console.WriteLine("Database already seeded. Skipping seeding.");
                return;
            }

            // These get created by the migration
            // Create roles if they don't exist
            // string[] roleNames = { "HousingAdmin", "User", "Renter" };
            // foreach (var roleName in roleNames)
            // {
            //     if (!await roleManager.RoleExistsAsync(roleName))
            //     {
            //         await roleManager.CreateAsync(new IdentityRole(roleName));
            //     }
            // }

            /****************************************************************************************
            *  Seed Data for Housing Management
            *
            *****************************************************************************************/
            // Create default users
            var admin = await CreateUser(userManager, "admin@housing.com", "Admin123!", ["HousingAdmin"]);
            var user1 = await CreateUser(userManager, "user1@housing.com", "User123!", ["Renter"]);
            var user2 = await CreateUser(userManager, "user2@housing.com", "User123!", ["Renter"]);
            var user3 = await CreateUser(userManager, "user3@housing.com", "User123!", ["Renter"]);

            // Get the newly created manager and create default housing groups
            var housingGroup = new HousingGroup
            {
                ManagerID = admin!.Id,
                Name = "Default Housing Group",
                Address = "123 Main St",
                City = "Richmond",
                PostalCode = "V7E4H4"
            };
            context.HousingGroups.Add(housingGroup);
            await context.SaveChangesAsync();

            // Create renters
            var renter1 = new Renter
            {
                IdentityID = user1!.Id,
                Name = "John Doe",
                DateOfBirth = new DateTime(1980, 1, 1),
                PhoneNumber = "604-123-4567",
            };
            context.Renters.Add(renter1);
            var renter2 = new Renter
            {
                IdentityID = user2!.Id,
                Name = "Jane Doe",
                DateOfBirth = new DateTime(1985, 1, 1),
                PhoneNumber = "604-234-5678",
            };
            context.Renters.Add(renter2);
            var renter3 = new Renter
            {
                IdentityID = user3!.Id,
                Name = "Alice Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "604-345-6789",
            };
            context.Renters.Add(renter3);

            // Create contacts
            var contact1 = new Contact
            {
                RenterID = renter1.ID,
                Name = "James Doe",
                PhoneNumber = "604-144-4567",
                Email = "JamesDoe@contact.com"
            };
            context.Contacts.Add(contact1);
            var contact2 = new Contact
            {
                RenterID = renter2.ID,
                Name = "Janet Doe",
                PhoneNumber = "604-244-4567",
                Email = "JanetDoe@contact.com"
            };
            context.Contacts.Add(contact2);

            // Create Building
            var building = new Building
            {
                HousingGroupID = housingGroup.ID,
                NumUnits = 10,
                NumLockers = 10,
                NumParking = 10,
                Address = "123 Building St",
                City = "Richmond",
                PostalCode = "V7E4H4"
            };
            context.Buildings.Add(building);

            // Create Suite
            var suite = new Suite
            {
                HousingGroupID = housingGroup.ID,
                BuildingID = building.ID,
                IsAvailable = true,
                RentAmount = 1000,

                UnitNumber = 101,
                Floor = 1,
                Occupants = 1,
                Rooms = 3,
                Bathrooms = 2
            };
            context.Suites.Add(suite);
            
            // Create Asset Damage
            var assetDamage = new AssetDamage
            {
                AssetID = suite.ID,
                RenterID = renter1.ID,
                Description = "Fist shaped hole in the wall",
                RecordedDate = DateTime.Now,
            };
            context.AssetDamages.Add(assetDamage);

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "damageImage.png");
            var damageImage = new DamageImage
            {
                AssetDamageID = assetDamage.ID,
                Photo = File.Exists(imagePath) ? File.ReadAllBytes(imagePath) : Array.Empty<byte>()
            };
            context.DamageImages.Add(damageImage);

            // Create Locker
            var locker = new Locker
            {
                HousingGroupID = housingGroup.ID,
                BuildingID = building.ID,
                IsAvailable = true,
                RentAmount = 50,
                SuiteID = suite.ID,
                LockerSize = "Small",
                LockerNumber = 1
            };
            context.Lockers.Add(locker);
            suite.LockerID = locker.ID;

            // Create Parking Spot
            var parkingSpot = new ParkingSpot
            {
                HousingGroupID = housingGroup.ID,
                BuildingID = building.ID,
                IsAvailable = true,
                RentAmount = 100,
                SuiteID = suite.ID,
                SpotNumber = 1
            };
            context.ParkingSpots.Add(parkingSpot);
            suite.ParkingSpotID = parkingSpot.ID;


            //****************************************************************************************

            await context.SaveChangesAsync();

        }

        private static async Task<IdentityUser?> CreateUser(UserManager<IdentityUser> userManager, string email, string password, string[] roles)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null) {
                return user;
            }
            var newUser = new IdentityUser
            {
                UserName = email,
                Email = email
            };
            var result = await userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(newUser, roles);
                return newUser;
            }
            return null;
        }
    }
}
