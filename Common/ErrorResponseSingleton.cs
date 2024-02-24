using Model;

namespace Common
{
    public class ErrorResponseSingleton
    {
        public ErrorResponseSingleton()
        {
            ErrorResponse = new ErrorResponseDTO();
        }
        private static ErrorResponseSingleton? instance;
        public static ErrorResponseSingleton Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ErrorResponseSingleton();
                }
                return instance;
            }
        }

        public ErrorResponseDTO ErrorResponse {  get; set; }
    }
}
