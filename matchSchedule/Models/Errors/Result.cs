namespace matchSchedule.Models.Errors
{
    public class Result
    {
        private Result(bool isSuccess, Error error, object value)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error!", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error!", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;

        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error Error { get; }
        public object Value { get; } = null;


        public static Result Failure(Error error) => new(false, error, null);
        public static Result Success() => new(true, Error.None);
        public static Result Success(object value) => new(true, Error.None, value);
    }

    public sealed record Error(string Code, string Description)
    {
        public static Error None = new(string.Empty, string.Empty);
    }
}