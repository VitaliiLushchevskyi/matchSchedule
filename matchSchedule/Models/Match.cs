using System.ComponentModel.DataAnnotations;

namespace matchSchedule.Models
{
    public class Match
    {
        [Key]
        public Guid MatchId { get; set; }
        public Guid HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public Guid AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public DateTime MatchDateTime { get; set; }
        public Tournament Tournament { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public string Referee { get; set; }
        public string MatchStatus { get; set; }

    }
}
