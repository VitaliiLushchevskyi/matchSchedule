namespace matchSchedule.Models.Errors
{
    public class TournamentErrors : BaseErrors
    {
        public static readonly Error NotFoundTournament = new Error(
           "Tournament.NotFoundTournament", "The tournament not found!");
    }
}
