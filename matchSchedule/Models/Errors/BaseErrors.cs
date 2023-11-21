namespace matchSchedule.Models.Errors
{
    public abstract class BaseErrors
    {
        public static readonly Error BadRequest = new Error(
            "Base.BadRequest", "Something went wrong!");
        public static readonly Error NotFound = new Error(
           "Base.NotFound", "Not Found!");
    }
}
