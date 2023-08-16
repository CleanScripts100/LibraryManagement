using WebApi.Business.src.Dto;

namespace WebApi.Business.src.Abstractions
{
    public interface IAuthService
    {
        Task<string> VerifyCredentials(UserCredentialsDto credentials);
    }
}