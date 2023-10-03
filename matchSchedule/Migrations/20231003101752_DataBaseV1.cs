using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tamplateIdentity.Migrations
{
    public partial class DataBaseV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_players_teams_TeamId",
                table: "players");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "players",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "players",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "JerseyNumber",
                table: "players",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Height",
                table: "players",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "players",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_matches_AwayTeamId",
                table: "matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_HomeTeamId",
                table: "matches",
                column: "HomeTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_AwayTeamId",
                table: "matches",
                column: "AwayTeamId",
                principalTable: "teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_matches_teams_HomeTeamId",
                table: "matches",
                column: "HomeTeamId",
                principalTable: "teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_players_teams_TeamId",
                table: "players",
                column: "TeamId",
                principalTable: "teams",
                principalColumn: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_AwayTeamId",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_matches_teams_HomeTeamId",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "FK_players_teams_TeamId",
                table: "players");

            migrationBuilder.DropIndex(
                name: "IX_matches_AwayTeamId",
                table: "matches");

            migrationBuilder.DropIndex(
                name: "IX_matches_HomeTeamId",
                table: "matches");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "players",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "players",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JerseyNumber",
                table: "players",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Height",
                table: "players",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_players_teams_TeamId",
                table: "players",
                column: "TeamId",
                principalTable: "teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
