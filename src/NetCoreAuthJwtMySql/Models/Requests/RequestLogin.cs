namespace NetCoreAuthJwtMySql.Models.Requests
{
    public class RequestLogin : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
