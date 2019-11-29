namespace Demo.EmailNotifications.Entities
{
    public class BaseResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public BaseResponse(bool success, string message = default)
        {
            Success = success;
            Message = message;
        }
    }
}
