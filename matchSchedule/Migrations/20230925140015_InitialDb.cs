using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tamplateIdentity.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "coaches",
                columns: table => new
                {
                    CoachId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coaches", x => x.CoachId);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearFounded = table.Column<int>(type: "int", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "tournaments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoachTeam",
                columns: table => new
                {
                    CoachesCoachId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamsTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachTeam", x => new { x.CoachesCoachId, x.TeamsTeamId });
                    table.ForeignKey(
                        name: "FK_CoachTeam_coaches_CoachesCoachId",
                        column: x => x.CoachesCoachId,
                        principalTable: "coaches",
                        principalColumn: "CoachId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoachTeam_teams_TeamsTeamId",
                        column: x => x.TeamsTeamId,
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JerseyNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_players_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwayTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TournamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HomeTeamGoals = table.Column<int>(type: "int", nullable: false),
                    AwayTeamGoals = table.Column<int>(type: "int", nullable: false),
                    Referee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_matches_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "teams",
                        principalColumn: "TeamId");
                    table.ForeignKey(
                        name: "FK_matches_tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "tournaments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeamTournament",
                columns: table => new
                {
                    TeamsTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TournamentsWonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamTournament", x => new { x.TeamsTeamId, x.TournamentsWonId });
                    table.ForeignKey(
                        name: "FK_TeamTournament_teams_TeamsTeamId",
                        column: x => x.TeamsTeamId,
                        principalTable: "teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamTournament_tournaments_TournamentsWonId",
                        column: x => x.TournamentsWonId,
                        principalTable: "tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playerTeamsHistory",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playerTeamsHistory", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_playerTeamsHistory_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoachTeam_TeamsTeamId",
                table: "CoachTeam",
                column: "TeamsTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_TeamId",
                table: "matches",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_TournamentId",
                table: "matches",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_players_TeamId",
                table: "players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_playerTeamsHistory_PlayerId",
                table: "playerTeamsHistory",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamTournament_TournamentsWonId",
                table: "TeamTournament",
                column: "TournamentsWonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoachTeam");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "playerTeamsHistory");

            migrationBuilder.DropTable(
                name: "TeamTournament");

            migrationBuilder.DropTable(
                name: "coaches");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "tournaments");

            migrationBuilder.DropTable(
                name: "teams");
        }
    }
}
