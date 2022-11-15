using ASP.NET_Core_Web_Api.Models.Dto;
using ASP.NET_Core_Web_Api.Models.Requests;

namespace ASP.NET_Core_Web_Api.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

        public SessionDto GetSession(string sessionToken);
    }
}
