using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V_Tube.Persisitence.Migrations
{
    /// <inheritdoc />
    public partial class views_viewedby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoViews_Users_UserId",
                table: "VideoViews");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "VideoViews",
                newName: "VieweById");

            migrationBuilder.RenameIndex(
                name: "IX_VideoViews_UserId",
                table: "VideoViews",
                newName: "IX_VideoViews_VieweById");

            migrationBuilder.AlterColumn<int>(
                name: "DurationViewed",
                table: "VideoViews",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoViews_Users_VieweById",
                table: "VideoViews",
                column: "VieweById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoViews_Users_VieweById",
                table: "VideoViews");

            migrationBuilder.RenameColumn(
                name: "VieweById",
                table: "VideoViews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoViews_VieweById",
                table: "VideoViews",
                newName: "IX_VideoViews_UserId");

            migrationBuilder.AlterColumn<decimal>(
                name: "DurationViewed",
                table: "VideoViews",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoViews_Users_UserId",
                table: "VideoViews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
