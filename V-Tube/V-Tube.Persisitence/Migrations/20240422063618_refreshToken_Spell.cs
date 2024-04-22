using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V_Tube.Persisitence.Migrations
{
    /// <inheritdoc />
    public partial class refreshToken_Spell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshToen",
                table: "Users",
                newName: "RefreshToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "Users",
                newName: "RefreshToen");
        }
    }
}
