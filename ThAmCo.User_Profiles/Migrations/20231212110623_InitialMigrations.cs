using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.User_Profiles.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableFunds = table.Column<double>(type: "float", nullable: false),
                    UserAddedOnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    LocationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AvailableFunds", "City", "Email", "FirstName", "LastName", "LocationNumber", "PhoneNumber", "PostalCode", "State", "Street", "UserAddedOnDate", "UserType", "Username" },
                values: new object[,]
                {
                    { "055cbca5ec06", 2077.7199999999998, "MiddlesBrough", "sara.jones@example.com", "Sara", "Jones", "333", "7483278294", "TS24SE", "United Kingdom", "Cedar Street", new DateTime(2023, 11, 12, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5525), 1, "sara_jones" },
                    { "283903b3bfc3", 2433.1199999999999, "Townsville", "jane.smith@example.com", "Jane", "Smith", "456", "7987761427", "TS64KU", "United Kingdom", "Broadway", new DateTime(2023, 12, 2, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5362), 0, "jane_smith" },
                    { "2b603ab48c4a", 4433.1199999999999, "Villageville", "bob.jones@example.com", "Bob", "Jones", "89", "7632561256", "TS54JU", "United Kingdom", "Oak Street", new DateTime(2023, 11, 27, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5393), 0, "bob_jones" },
                    { "324e27ec87f9", 433.31999999999999, "Cityville", "john.doe@example.com", "John", "Doe", "123", "7932124382", "TS64KU", "United Kingdom", "Main Street", new DateTime(2023, 12, 7, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5273), 0, "john_doe" },
                    { "79becad34f5b", 577.72000000000003, "MiddlesBrough", "alice.smith@example.com", "Alice", "Smith", "101", "7982837112", "TS14JU", "United Kingdom", "Maple Street", new DateTime(2023, 11, 22, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5424), 0, "alice_smith" },
                    { "a86a33bc4245", 5077.7200000000003, "MiddlesBrough", "jatinaneja2000@outlook.com", "Jatin", "Aneja", "222", "7263712161", "TS14JE", "United Kingdom", "Pine Street", new DateTime(2023, 11, 17, 11, 6, 23, 110, DateTimeKind.Utc).AddTicks(5452), 1, "JatinAneja01" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
