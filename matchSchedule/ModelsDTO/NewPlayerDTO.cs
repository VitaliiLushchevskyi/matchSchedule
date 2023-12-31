﻿namespace matchSchedule.ModelsDTO
{
    public class NewPlayerDTO
    {
        public Guid PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Position { get; set; }
        public int JerseyNumber { get; set; }
        public Guid? TeamId { get; set; }
    }
}
