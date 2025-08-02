using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvailableCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableCategories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AvailableCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Электроника" },
                    { 2, "Одежда" },
                    { 3, "Еда" },
                    { 4, "Развлечения" },
                    { 5, "Медицина" },
                    { 6, "Путешествия" },
                    { 7, "Транспорт" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailableCategories");
        }
    }
}
