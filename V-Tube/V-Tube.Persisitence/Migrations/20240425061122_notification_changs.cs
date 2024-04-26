using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V_Tube.Persisitence.Migrations
{
    /// <inheritdoc />
    public partial class notification_changs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SubscribedBy",
                table: "Notifications");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscribedBy",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "LikedBy",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LikedBy",
                table: "Notifications",
                column: "LikedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_LikedBy",
                table: "Notifications",
                column: "LikedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_SubscribedBy",
                table: "Notifications",
                column: "SubscribedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_LikedBy",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SubscribedBy",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_LikedBy",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "LikedBy",
                table: "Notifications");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscribedBy",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_SubscribedBy",
                table: "Notifications",
                column: "SubscribedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
