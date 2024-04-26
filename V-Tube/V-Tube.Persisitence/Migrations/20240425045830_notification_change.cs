using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V_Tube.Persisitence.Migrations
{
    /// <inheritdoc />
    public partial class notification_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommentId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubscribedBy",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable:true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VideoId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SubscribedBy",
                table: "Notifications",
                column: "SubscribedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_SubscribedBy",
                table: "Notifications",
                column: "SubscribedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SubscribedBy",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SubscribedBy",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SubscribedBy",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Notifications");
        }
    }
}
