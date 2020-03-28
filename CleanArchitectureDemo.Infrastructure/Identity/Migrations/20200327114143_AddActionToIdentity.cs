using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitectureDemo.Infrastructure.Identity.Migrations
{
    public partial class AddActionToIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllActionsEnabled",
                schema: "security",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Actions",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    ControllerName = table.Column<string>(maxLength: 150, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleActions",
                schema: "security",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    ActionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleActions", x => new { x.RoleId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_RoleActions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalSchema: "security",
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleActions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleActions_ActionId",
                schema: "security",
                table: "RoleActions",
                column: "ActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleActions",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Actions",
                schema: "security");

            migrationBuilder.DropColumn(
                name: "AllActionsEnabled",
                schema: "security",
                table: "Roles");
        }
    }
}
