using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V_Tube.Persisitence.Migrations
{
    /// <inheritdoc />
    public partial class commentReplies_replied_by : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RepliedBy",
                table: "CommentReplies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_RepliedBy",
                table: "CommentReplies",
                column: "RepliedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_Users_RepliedBy",
                table: "CommentReplies",
                column: "RepliedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_Users_RepliedBy",
                table: "CommentReplies");

            migrationBuilder.DropIndex(
                name: "IX_CommentReplies_RepliedBy",
                table: "CommentReplies");

            migrationBuilder.DropColumn(
                name: "RepliedBy",
                table: "CommentReplies");
        }
    }
}
