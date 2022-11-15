using ASP.NET_Core_Web_Api.Models.Dto;

namespace ASP.NET_Core_Web_Api.Models.Requests
{
    public class AuthenticationResponse
    {
        public AuthenticationStatus Status { get; set; }
        public SessionDto Session { get; set; }
    }
}
