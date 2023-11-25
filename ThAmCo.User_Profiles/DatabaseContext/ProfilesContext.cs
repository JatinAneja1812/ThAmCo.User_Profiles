﻿using Microsoft.EntityFrameworkCore;
using ThAmCo.User_Profiles.Enums;
using ThAmCo.User_Profiles.Models;

namespace ThAmCo.User_Profiles.DatabaseContext
{
    public class ProfilesContext : DbContext
    {
        public ProfilesContext() : base()
        {
        }

        public ProfilesContext(DbContextOptions<ProfilesContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed fake user data
            modelBuilder.Entity<User>().HasData(
                 // Customers
                 new User
                 {
                     UserId = Guid.NewGuid(),
                     Username = "john_doe",
                     Email = "john.doe@example.com",
                     FirstName = "John",
                     LastName = "Doe",
                     AvailableFunds = 433.32,
                     PhoneNumber = "1234567890",
                     UserType = UserTypeEnum.Customer,
                     LocationNumber = "123",
                     Street = "Main Street",
                     City = "Cityville",
                     State = "United Kingdoms",
                     PostalCode = "TS64KU",
                     UserAddedOnDate = DateTime.Now.AddDays(-5),
                 },
                 new User
                 {
                     UserId = Guid.NewGuid(),
                     Username = "jane_smith",
                     Email = "jane.smith@example.com",
                     FirstName = "Jane",
                     LastName = "Smith",
                     AvailableFunds = 2433.12,
                     PhoneNumber = "9876543210",
                     UserType = UserTypeEnum.Customer,
                     LocationNumber = "456",
                     Street = "Broadway",
                     City = "Townsville",
                     State = "United Kingdoms",
                     PostalCode = "TS64KU",
                     UserAddedOnDate = DateTime.Now.AddDays(-10)
                 },
                 new User
                 {
                     UserId = Guid.NewGuid(),
                     Username = "bob_jones",
                     Email = "bob.jones@example.com",
                     FirstName = "Bob",
                     LastName = "Jones",
                     AvailableFunds = 4433.12,
                     PhoneNumber = "5555555555",
                     UserType = UserTypeEnum.Customer,
                     LocationNumber = "89",
                     Street = "Oak Street",
                     City = "Villageville",
                     State = "United Kingdoms",
                     PostalCode = "TS54JU",
                     UserAddedOnDate = DateTime.Now.AddDays(-15)
                 },
                 new User
                 {
                     UserId = Guid.NewGuid(),
                     Username = "alice_smith",
                     Email = "alice.smith@example.com",
                     FirstName = "Alice",
                     LastName = "Smith",
                     AvailableFunds = 577.72,
                     PhoneNumber = "4444444444",
                     UserType = UserTypeEnum.Customer,
                     LocationNumber = "101",
                     Street = "Maple Street",
                     City = "MiddlesBrough",
                     State = "United Kingdoms",
                     PostalCode = "TS14JU"
                     UserAddedOnDate = DateTime.Now.AddDays(-20)
                 },
                 // Staff
                 new User
                 {
                     UserId = Guid.NewGuid(),
                     Username = "JatinAneja01",
                     Email = "jatinaneja2000@outlook.com",
                     FirstName = "Jatin",
                     LastName = "Aneja",
                     AvailableFunds = 5077.72,
                     PhoneNumber = "7263712161",
                     UserType = UserTypeEnum.Staff,
                     LocationNumber = "222",
                     Street = "Pine Street",
                     City = "MiddlesBrough",
                     State = "United Kingdoms",
                     PostalCode = "TS14JE",
                     UserAddedOnDate = DateTime.Now.AddDays(-25)
                 },
                 new User
                 {
                     UserId = Guid.NewGuid(),
                     Username = "sara_jones",
                     Email = "sara.jones@example.com",
                     FirstName = "Sara",
                     LastName = "Jones",
                     AvailableFunds = 2077.72,
                     PhoneNumber = "9999999999",
                     UserType = UserTypeEnum.Staff,
                     LocationNumber = "333",
                     Street = "Cedar Street",
                     City = "MiddlesBrough",
                     State = "United Kingdoms",
                     PostalCode = "TS24SE",
                     UserAddedOnDate = DateTime.Now.AddDays(-30)
                 }
             );
        }
    }
}