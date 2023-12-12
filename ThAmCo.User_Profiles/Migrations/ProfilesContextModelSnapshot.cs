﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.User_Profiles.DatabaseContext;

#nullable disable

namespace ThAmCo.User_Profiles.Migrations
{
    [DbContext(typeof(ProfilesContext))]
    partial class ProfilesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ThAmCo.User_Profiles.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("AvailableFunds")
                        .HasColumnType("float");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserAddedOnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = "324e27ec87f9",
                            AvailableFunds = 433.31999999999999,
                            City = "Cityville",
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            LocationNumber = "123",
                            PhoneNumber = "7932124382",
                            PostalCode = "TS64KU",
                            State = "United Kingdom",
                            Street = "Main Street",
                            UserAddedOnDate = new DateTime(2023, 12, 7, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5273),
                            UserType = 0,
                            Username = "john_doe"
                        },
                        new
                        {
                            UserId = "283903b3bfc3",
                            AvailableFunds = 2433.1199999999999,
                            City = "Townsville",
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            LocationNumber = "456",
                            PhoneNumber = "7987761427",
                            PostalCode = "TS64KU",
                            State = "United Kingdom",
                            Street = "Broadway",
                            UserAddedOnDate = new DateTime(2023, 12, 2, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5362),
                            UserType = 0,
                            Username = "jane_smith"
                        },
                        new
                        {
                            UserId = "2b603ab48c4a",
                            AvailableFunds = 4433.1199999999999,
                            City = "Villageville",
                            Email = "bob.jones@example.com",
                            FirstName = "Bob",
                            LastName = "Jones",
                            LocationNumber = "89",
                            PhoneNumber = "7632561256",
                            PostalCode = "TS54JU",
                            State = "United Kingdom",
                            Street = "Oak Street",
                            UserAddedOnDate = new DateTime(2023, 11, 27, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5393),
                            UserType = 0,
                            Username = "bob_jones"
                        },
                        new
                        {
                            UserId = "79becad34f5b",
                            AvailableFunds = 577.72000000000003,
                            City = "MiddlesBrough",
                            Email = "alice.smith@example.com",
                            FirstName = "Alice",
                            LastName = "Smith",
                            LocationNumber = "101",
                            PhoneNumber = "7982837112",
                            PostalCode = "TS14JU",
                            State = "United Kingdom",
                            Street = "Maple Street",
                            UserAddedOnDate = new DateTime(2023, 11, 22, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5424),
                            UserType = 0,
                            Username = "alice_smith"
                        },
                        new
                        {
                            UserId = "a86a33bc4245",
                            AvailableFunds = 5077.7200000000003,
                            City = "MiddlesBrough",
                            Email = "jatinaneja2000@outlook.com",
                            FirstName = "Jatin",
                            LastName = "Aneja",
                            LocationNumber = "222",
                            PhoneNumber = "7263712161",
                            PostalCode = "TS14JE",
                            State = "United Kingdom",
                            Street = "Pine Street",
                            UserAddedOnDate = new DateTime(2023, 11, 17, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5452),
                            UserType = 1,
                            Username = "JatinAneja01"
                        },
                        new
                        {
                            UserId = "055cbca5ec06",
                            AvailableFunds = 2077.7199999999998,
                            City = "MiddlesBrough",
                            Email = "sara.jones@example.com",
                            FirstName = "Sara",
                            LastName = "Jones",
                            LocationNumber = "333",
                            PhoneNumber = "7483278294",
                            PostalCode = "TS24SE",
                            State = "United Kingdom",
                            Street = "Cedar Street",
                            UserAddedOnDate = new DateTime(2023, 11, 12, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5525),
                            UserType = 1,
                            Username = "sara_jones"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
