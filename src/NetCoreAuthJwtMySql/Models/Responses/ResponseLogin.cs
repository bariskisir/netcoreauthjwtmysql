namespace NetCoreAuthJwtMySql.Models.Responses
{
    public class ResponseLogin : BaseResponse
    {
        public string Token { get; set; }
        public int ExpireDate { get; set; }
    }
}
