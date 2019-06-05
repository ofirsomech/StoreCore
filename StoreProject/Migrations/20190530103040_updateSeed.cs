using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreProject.Migrations
{
    public partial class updateSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { 1L, new DateTime(2019, 5, 30, 13, 30, 39, 707, DateTimeKind.Local), "user@gmail.com", "Ofir", "Somech", "123456", "user@gmail.com" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { 2L, new DateTime(2019, 5, 30, 13, 30, 39, 710, DateTimeKind.Local), "avi@gmail.com", "Avi", "Levi", "123456", "avi@gmail.com" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { 3L, new DateTime(2019, 5, 30, 13, 30, 39, 710, DateTimeKind.Local), "dani@gmail.com", "Dani", "Cohen", "123456", "dani@gmail.com" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Date", "Img1", "Img2", "Img3", "LongDescription", "OwnerId", "Price", "ShortDescription", "State", "Title", "UserId" },
                values: new object[] { 1L, new DateTime(2019, 5, 30, 13, 30, 39, 711, DateTimeKind.Local), null, null, null, "test", 3L, 33m, "TEST1", 0, "title", 2L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
