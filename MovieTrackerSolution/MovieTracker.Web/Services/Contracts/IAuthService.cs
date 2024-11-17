using Auth.Models;

namespace MovieTracker.Web.Services.Contracts
{
    public interface IAuthService
    {

        Task<HttpResponseMessage> Login(LoginRequest loginRequest);

    }
}
