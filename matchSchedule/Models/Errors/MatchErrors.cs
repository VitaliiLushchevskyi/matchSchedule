namespace matchSchedule.Models.Errors
{
    public class MatchErrors : BaseErrors
    {

        public static readonly Error SameTeams = new Error(
            "Matches.SameTeam", "Can`t create match with two identical teams!");

        public static readonly Error NotFoundTournament = new Error(
            "Matches.NotFoundTournament", "The tournament not found!");



    }
}
