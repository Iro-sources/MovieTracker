using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;

namespace MovieTracker.Web.Services.Contracts
{
    public interface IUserService
    {

        Task<UserReadDto?> GetUser(string Id, string Token);

        Task<HttpResponseMessage> CreateNewUser(UserSignUpDto userSignUpDto);

        Task<HttpResponseMessage> UpdateUserGenrePreferences(string userId, Dictionary<string, bool> preferences);
    }
}
