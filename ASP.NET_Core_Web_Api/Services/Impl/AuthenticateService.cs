using AccountHelper;
using ASP.NET_Core_Web_Api.Models.Dto;
using ASP.NET_Core_Web_Api.Models.Requests;
using EmployeeService.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP.NET_Core_Web_Api.Services.Impl
{
    public class AuthenticateService : IAuthenticateService
    {
        public const string SecretKey = "B=o1№#lO@D1m19SkuF10On!";

        private readonly Dictionary<string, SessionDto> _sessions = new Dictionary<string, SessionDto>();

        private readonly IServiceScopeFactory _serviceScopeFactory;


        public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

        }

        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            EmployeeServiceDbContext context = scope.ServiceProvider.GetRequiredService<EmployeeServiceDbContext>();

            Account account = !string.IsNullOrWhiteSpace(authenticationRequest.Login) ?
                FindAccountByLogin(context, authenticationRequest.Login) : null;
            if (account == null)
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.UserNotFound
                };
            }
            
            if(!PasswordUtils.VerifyPassword(authenticationRequest.Password,account.PasswordSalt,account.PasswordSalt))
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.InvalidPassword
                };
            }

            AccountSession session = new AccountSession
            {
                AccountId = account.AccountId,
                SessionToken = CreateSessionToken(account),
                TimeCreated = DateTime.Now,
                TimeClosed = DateTime.Now,
                IsClosed = false,
            };
            
            context.AccountSessions.Add(session);
            context.SaveChanges();

            SessionDto sessionDto = GetSessionDto(account, session);
            lock (_sessions)
            {
                _sessions[session.SessionToken] = sessionDto;
            }

            return new AuthenticationResponse { Status = AuthenticationStatus.Success,Session = sessionDto};

        }

        private SessionDto GetSessionDto(Account account,AccountSession accountSessions)
        {
            return new SessionDto
            {
                SessionId = accountSessions.SessionId,
                SessionToken = accountSessions.SessionToken,
                Account = new AccountDto
                {
                    AccountId = account.AccountId,
                    EMail = account.EMail,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    SecondName = account.SecondName,
                    Locked = account.Locked
                }
            };
        }


        private string CreateSessionToken(Account account)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                        new Claim(ClaimTypes.Name,account.EMail)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.EcdsaSha512Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private Account FindAccountByLogin(EmployeeServiceDbContext context, string login)
        {
            return context.Accounts.FirstOrDefault(account => account.EMail == login);
        }

        public SessionDto GetSession(string sessionToken)
        {
            throw new NotImplementedException();
        }
    }
}
