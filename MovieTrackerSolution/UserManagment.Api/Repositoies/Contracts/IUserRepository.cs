using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using UserManagment.Api.Entities;

namespace UserManagment.Api.Repositoies.Contracts
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> GetUsers();

        Task<User?> GetUserById(string id);

        Task<IEnumerable<ConfigMovieGenre>> GetUserMovieGenreConfig(string userId);


        Task<User?> GetUserByEmail(string email);


        Task UpdateUserGenrePreference(string userId, Dictionary<string, bool> preferences);


        Task<UserSignUpDto> CreateNewUser(UserSignUpDto userSignUpDto);
    }
}
