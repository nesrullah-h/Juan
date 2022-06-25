using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CreatDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IconUrl = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false,defaultValue:false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyProperty", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MyProperty",
                columns: new[] { "Id", "Description", "IconUrl", "IsDeleted", "Title" },
                values: new object[] { 1, "orem ipsum dolizzle sit tellivizzle, boom shackalack adipiscing elit. Nullam mofo velizzle, get down get down stuff, sure cool, dizzle vizzle, arcu. Yo mamma izzle tortizzle.", "<i class=fas fa - tablet - alt></i>", false, "Responsive Bootstrap Template" });

            migrationBuilder.InsertData(
                table: "MyProperty",
                columns: new[] { "Id", "Description", "IconUrl", "IsDeleted", "Title" },
                values: new object[] { 2, "orem ipsum dolizzle sit tellivizzle, boom shackalack adipiscing elit. Nullam mofo velizzle, get down get down stuff, sure cool, dizzle vizzle, arcu. Yo mamma izzle tortizzle.", "<i class=fas fa - tablet - alt></i>", false, "Responsive Bootstrap Template" });

            migrationBuilder.InsertData(
                table: "MyProperty",
                columns: new[] { "Id", "Description", "IconUrl", "IsDeleted", "Title" },
                values: new object[] { 3, "orem ipsum dolizzle sit tellivizzle, boom shackalack adipiscing elit. Nullam mofo velizzle, get down get down stuff, sure cool, dizzle vizzle, arcu. Yo mamma izzle tortizzle.", "<i class=fas fa - tablet - alt></i>", false, "Responsive Bootstrap Template" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyProperty");
        }
    }
}
