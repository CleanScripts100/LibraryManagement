using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dto;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Shared
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;

        public AuthService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<string> VerifyCredentials(UserCredentialsDto credentials)
        {
            var foundUserByEmail = await _userRepo.FindOneByEmail(credentials.Email) ?? throw new Exception("Email not found");
            var isAuthenticated = PasswordService.VerifyPassword(credentials.Password, foundUserByEmail.Password!, foundUserByEmail.Salt);
            if (!isAuthenticated)
            {
                throw ServiceException.UnAuthenticatedException();
            }
            return GenerateToken(foundUserByEmail);
        }

        private static string GenerateToken(User user)
        {
            /* should find a way to move the logic to webapi layer */
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my-secrete-key-jsdguyfsdgcjsdbchjsdb"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "libraryManagement",
                Audience = "resource",
                Expires = DateTime.Now.AddMinutes(30),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}