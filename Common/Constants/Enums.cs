namespace Common.Constants
{
    public class Enums
    {
        public enum Severity
        {
            CRITICAL,
            MAJOR,
            MODERATE,
            MINOR,
        }

        public enum NotificationTypes
        {
            INFO = 250, 
            WARNING = 150, 
            VALIDATIONFAILURE = 450, 
            NOCONTENT = 204, 
            APIERROR = 512, 
            BADREQUEST = 400, 
            OK = 200, 
            INTERNALSERVERERROR = 500, 
            ExpectationFailed = 417, 
            AccessForbidden = 403,
        }
    }
}
