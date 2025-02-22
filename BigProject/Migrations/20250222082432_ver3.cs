using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigProject.Migrations
{
    /// <inheritdoc />
    public partial class ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RewardOrDiscipline",
                table: "rewardDisciplineTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RewardOrDiscipline",
                table: "rewardDisciplineTypes");
        }
    }
}
