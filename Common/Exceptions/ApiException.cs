

using static Common.Constants.Constant;
using static Common.Constants.Enums;

namespace Common.Exceptions
{
    public class ApiException : Exception
    {
        public NotificationTypes StatusCode { get; set; }
        public Severity Severity { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsCustomException { get; set; }
        public string? Parameters { get; set; }
        public bool IsHandled { get; set; }
        public string? Location { get; set; }
        public string? Code { get; set; }
        public string? StatusCodeText { get; set; }
        public ApiException()
        {
            StatusCode = NotificationTypes.INTERNALSERVERERROR;
            Severity = Severity.MINOR;
        }

        public ApiException(
            string message,
            Exception e,
            string parameters = "",
            NotificationTypes statusCode = NotificationTypes.INTERNALSERVERERROR,
            string StatusCodeText = CustomHttpStatusText.VALIDATION_FAILURE) : base(message, e)
        {
            ErrorMessage = message;
            StatusCode = statusCode;
            Severity = Severity.MINOR;
            IsCustomException = true;
            Parameters = parameters;
            this.StatusCodeText = StatusCodeText;
        }

        public ApiException(
            string message,
            string parameters = "",
            Severity severity = Severity.MINOR,
            bool isHandled = true,
            NotificationTypes statusCode = NotificationTypes.INTERNALSERVERERROR,
            string statusCodeText = CustomHttpStatusText.VALIDATION_FAILURE)
        {
            ErrorMessage = message;
            IsCustomException = true;
            StatusCode = statusCode;
            Severity = severity;
            IsHandled = isHandled;
            Parameters = parameters;
            StatusCodeText = statusCodeText;
        }
    }
}
