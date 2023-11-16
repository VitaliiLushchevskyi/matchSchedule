using matchSchedule.Models;
using System.ComponentModel.DataAnnotations;

namespace matchSchedule.ModelsDTO
{
    public class NewMatchDTO
    {
        [Key]
        public Guid MatchId { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public DateTime MatchDateTime { get; set; }
        public Tournament Tournament { get; set; }
        public string Referee { get; set; }
    }
}
