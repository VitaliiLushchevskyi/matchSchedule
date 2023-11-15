namespace matchSchedule.Models.Errors
{
    public class MatchErrors
    {

        public static readonly Error SameTeams = new Error(
            "Matches.SameTeam", "Can`t create match with two identical teams!");

        public static readonly Error NotFoundTournament = new Error(
            "Matches.NotFoundTournament", "The tournament not found!");

        public static readonly Error BadRequest = new Error(
            "Matches.BadRequest", "Something went wrong!");

    }
}
