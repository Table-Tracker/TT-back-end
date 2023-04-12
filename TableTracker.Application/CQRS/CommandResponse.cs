using TableTracker.Domain.Enums;

namespace TableTracker.Application.CQRS
{
    public class CommandResponse<T>  where T : class
    {
        public CommandResponse(
            T obj = null,
            CommandResult result = CommandResult.Success,
            string errorMessage = "")
        {
            Object = obj;
            Result = result;
            ErrorMessage = errorMessage;
        }

        public CommandResult Result { get; set; }

        public T Object { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class CommandResponse
    {
        public CommandResponse(
            CommandResult result = CommandResult.Success,
            string errorMessage = "")
        {
            Result = result;
            ErrorMessage = errorMessage;
        }

        public CommandResult Result { get; set; }

        public string ErrorMessage { get; set; }
    }
}
