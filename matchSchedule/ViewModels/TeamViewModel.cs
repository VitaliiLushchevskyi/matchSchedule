using matchSchedule.Models;

namespace matchSchedule.ViewModels
{
    public class TeamViewModel
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int YearFounded { get; set; }
        public string Logo { get; set; }
       
    }
}
