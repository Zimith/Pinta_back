using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pinta.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Image_AvatarId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Image_BannerId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "Width",
                table: "Files",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Files",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Files",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");
            migrationBuilder.DropTable(
                name: "Follows");
            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserFollowingId = table.Column<int>(type: "int", nullable: true),
                    UserFollowedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follows_Persons_UserFollowedId",
                        column: x => x.UserFollowedId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Follows_Persons_UserFollowingId",
                        column: x => x.UserFollowingId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_UserFollowedId",
                table: "Follows",
                column: "UserFollowedId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_UserFollowingId",
                table: "Follows",
                column: "UserFollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Files_AvatarId",
                table: "Persons",
                column: "AvatarId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Files_BannerId",
                table: "Persons",
                column: "BannerId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Files_AvatarId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Files_BannerId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "Image");

            migrationBuilder.AlterColumn<int>(
                name: "Width",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Image",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Image_AvatarId",
                table: "Persons",
                column: "AvatarId",
                principalTable: "Image",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Image_BannerId",
                table: "Persons",
                column: "BannerId",
                principalTable: "Image",
                principalColumn: "Id");
        }
    }
}
