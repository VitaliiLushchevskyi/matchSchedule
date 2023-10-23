using System.ComponentModel.DataAnnotations;

namespace matchSchedule.Models
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int YearFounded { get; set; }
        public string Logo { get; set; }
        public ICollection<Player> Players { get; set; } 
        public ICollection<Coach> Coaches { get; set; } 
        public ICollection<Match> Matches { get; set; } 
        public ICollection<Tournament> TournamentsWon { get; set; } 

    }
}
