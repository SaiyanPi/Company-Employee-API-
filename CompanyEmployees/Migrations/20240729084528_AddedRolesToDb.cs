using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployees.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "03a26fd6-d289-4749-8df8-c98a5c10d6ca", "785d3aa8-2f1e-4364-bc74-7d412cec589c", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f15273ec-6e87-4dde-9806-e1931935a2e3", "6ef227f6-7163-4790-a675-ca54a2490f08", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03a26fd6-d289-4749-8df8-c98a5c10d6ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f15273ec-6e87-4dde-9806-e1931935a2e3");
        }
    }
}
