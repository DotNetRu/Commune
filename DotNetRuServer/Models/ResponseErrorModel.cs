namespace DotNetRuServer.Models
{
    public class ResponseErrorModel
    {
        public ResponseErrorModel(string msg, int code)
        {
            Message = msg;
            StatusCode = code;
        }

        public int StatusCode { get; }

        public string StackTrace { get; set; }

        public string Message { get; }
    }
}
