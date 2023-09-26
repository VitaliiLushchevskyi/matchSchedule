using System.ComponentModel.DataAnnotations;

namespace matchSchedule.Models
{
    public class PlayerTeamHistory
    {
        [Key]
        public Guid HistoryId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid TeamId { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LeaveDate { get; set; }
    }
}
