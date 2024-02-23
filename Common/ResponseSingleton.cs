using Model;

namespace Common
{
    public class ResponseSingleton
    {
        public ResponseSingleton()
        {
            this.Response = new ResponseDTO();
        }
        private static ResponseSingleton? instance;
        public static ResponseSingleton Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ResponseSingleton();
                }
                return instance;
            }
        }

        public ResponseDTO Response { get; set; }
    }
}
