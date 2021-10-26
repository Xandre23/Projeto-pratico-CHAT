namespace ChatUni9.Models
{
    public class HttpResponseViewlModel
    {       
        public int Code { get; set; }
        public string Message { get; set; }
        public HttpResponseViewlModel(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
