using System.ComponentModel.DataAnnotations;

namespace matchSchedule.Models
{
    public class Coach
    {
        [Key]
        public Guid CoachId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public ICollection<Team> Teams { get; set; }

    }
}
