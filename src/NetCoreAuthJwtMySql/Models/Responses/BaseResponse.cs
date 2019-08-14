using System.Collections.Generic;
namespace NetCoreAuthJwtMySql.Models.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public List<string> MessageList { get; set; }
        public BaseResponse()
        {
            MessageList = new List<string>();
        }
    }
}
