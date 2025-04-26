using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Pitches",
                newName: "CitysId");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Pitches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Citys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pitches_CitysId",
                table: "Pitches",
                column: "CitysId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pitches_Citys_CitysId",
                table: "Pitches",
                column: "CitysId",
                principalTable: "Citys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pitches_Citys_CitysId",
                table: "Pitches");

            migrationBuilder.DropTable(
                name: "Citys");

            migrationBuilder.DropIndex(
                name: "IX_Pitches_CitysId",
                table: "Pitches");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Pitches");

            migrationBuilder.RenameColumn(
                name: "CitysId",
                table: "Pitches",
                newName: "City");
        }
    }
}
