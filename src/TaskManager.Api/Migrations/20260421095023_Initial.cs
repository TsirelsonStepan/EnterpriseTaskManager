using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserGuid = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    UserData = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserGuid);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskGuid = table.Column<string>(type: "TEXT", nullable: false),
                    TaskName = table.Column<string>(type: "TEXT", nullable: false),
                    TaskStatus = table.Column<string>(type: "TEXT", nullable: false),
                    TaskDescrition = table.Column<string>(type: "TEXT", nullable: true),
                    TaskDeadline = table.Column<string>(type: "TEXT", nullable: true),
                    TaskOwnerUserGuid = table.Column<string>(type: "TEXT", nullable: false),
                    TaskOwnerUserUserGuid = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskGuid);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_TaskOwnerUserUserGuid",
                        column: x => x.TaskOwnerUserUserGuid,
                        principalTable: "Users",
                        principalColumn: "UserGuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskOwnerUserUserGuid",
                table: "Tasks",
                column: "TaskOwnerUserUserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
