using Common.Constants;
namespace Common.Helpers
{
    public class ApiResponseHelper
    {
        private readonly ResponseSingleton content = ResponseSingleton.Instance;
        private readonly ErrorResponseSingleton errorContent = ErrorResponseSingleton.Instance;

        public ErrorResponseSingleton ApiErrorResponse(string? errorMessage, string path)
        {
            errorContent.ErrorResponse.Title = Message.ERROR_TITLE;
            errorContent.ErrorResponse.Status = (int?)Enums.NotificationTypes.BADREQUEST;
            errorContent.ErrorResponse.Detail = errorMessage;
            errorContent.ErrorResponse.Instances = path;
            return errorContent;
        }

        public ResponseSingleton ApiSuccessResponse(string customMessage)
        {
            content.Response.Success = true;
            content.Response.Message = customMessage;
            content.Response.Data = null;
            return content;
        }

        public ResponseSingleton ApiGetSuccessResponse(object listObject)
        {
            content.Response.Success = true;
            content.Response.Message = Message.OK_MESSAGE;
            content.Response.Data = new { listObject };
            return content;
        }
    }
}
