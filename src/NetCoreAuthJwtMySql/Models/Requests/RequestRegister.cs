namespace NetCoreAuthJwtMySql.Models.Requests
{
    public class RequestRegister : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
