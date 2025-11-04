using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Divisima.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BlogUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogBlogCategory_BlogCategory_BlogCategoryID",
                table: "BlogBlogCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogBlogCategory",
                table: "BlogBlogCategory");

            migrationBuilder.DropColumn(
                name: "BlogCattegoryID",
                table: "BlogBlogCategory");

            migrationBuilder.AlterColumn<int>(
                name: "BlogCategoryID",
                table: "BlogBlogCategory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogBlogCategory",
                table: "BlogBlogCategory",
                columns: new[] { "BlogID", "BlogCategoryID" });

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2023, 7, 27, 1, 38, 30, 117, DateTimeKind.Local).AddTicks(2570));

            migrationBuilder.AddForeignKey(
                name: "FK_BlogBlogCategory_BlogCategory_BlogCategoryID",
                table: "BlogBlogCategory",
                column: "BlogCategoryID",
                principalTable: "BlogCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogBlogCategory_BlogCategory_BlogCategoryID",
                table: "BlogBlogCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogBlogCategory",
                table: "BlogBlogCategory");

            migrationBuilder.AlterColumn<int>(
                name: "BlogCategoryID",
                table: "BlogBlogCategory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BlogCattegoryID",
                table: "BlogBlogCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogBlogCategory",
                table: "BlogBlogCategory",
                columns: new[] { "BlogID", "BlogCattegoryID" });

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2023, 7, 27, 1, 34, 39, 121, DateTimeKind.Local).AddTicks(7776));

            migrationBuilder.AddForeignKey(
                name: "FK_BlogBlogCategory_BlogCategory_BlogCategoryID",
                table: "BlogBlogCategory",
                column: "BlogCategoryID",
                principalTable: "BlogCategory",
                principalColumn: "ID");
        }
    }
}
