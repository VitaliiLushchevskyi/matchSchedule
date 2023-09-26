using System.ComponentModel.DataAnnotations;

namespace matchSchedule.Models
{
    public class Tournament
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public ICollection<Match> Matches { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
