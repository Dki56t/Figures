using Microsoft.EntityFrameworkCore.Migrations;

namespace Implementation.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "FigureInfos",
                table => new
                {
                    Id = table.Column<long>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Figure = table.Column<string>("TEXT", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_FigureInfos", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("FigureInfos");
        }
    }
}