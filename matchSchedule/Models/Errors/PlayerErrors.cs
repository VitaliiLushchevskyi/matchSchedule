namespace matchSchedule.Models.Errors
{
    public class PlayerErrors : BaseErrors
    {
        public static readonly Error NoFreePlayers = new Error(
           "Players.NoFreePlayers", "There are no free players! You can create one or remove the desired player from the team!");
    }
}
