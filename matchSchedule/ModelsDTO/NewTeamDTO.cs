namespace matchSchedule.ModelsDTO
{
    public class NewTeamDTO
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int YearFounded { get; set; }
        public string Logo { get; set; }
        public ICollection<Guid> PlayerIds { get; set; }
    }
}
