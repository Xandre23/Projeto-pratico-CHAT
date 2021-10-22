using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatUni9.Models
{
    public class HttpResponse
    {       
        public int Code { get; set; }
        public string Message { get; set; }
        public HttpResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
