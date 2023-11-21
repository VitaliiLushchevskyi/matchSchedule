namespace matchSchedule.Models.Errors
{
    public class TeamErrors : BaseErrors
    {
        public static readonly Error NoTeams = new Error(
             "Matches.NoTeams", "No teams found");

        public static readonly Error AlreadyExist = new Error(
             "Matches.AlreadyExist", "This team already exist!");

    }
}
