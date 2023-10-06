using matchSchedule.Models;
using System.ComponentModel.DataAnnotations;

namespace matchSchedule.ViewModels
{
    public class TournamentViewModel
    {
        [Key]
        public Guid TournamentId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public ICollection<Guid> TeamIds { get; set; }
       
    }
}
