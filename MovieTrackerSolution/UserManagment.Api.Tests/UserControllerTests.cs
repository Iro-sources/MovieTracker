using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;

using System.Text;

using UserManagment.Api.Data;

namespace UserManagment.Api.Tests;

public class UserControllerTests : IClassFixture<IntegrationTestFactory<Program, UserDbContext>>
{
    private readonly IntegrationTestFactory<Program, UserDbContext> _factory;

    public UserControllerTests(IntegrationTestFactory<Program, UserDbContext> factory) => _factory = factory;

    [Fact]
    public async Task GetUsers_ShouldReturnOk_WhenUsersExist()
    {
        const string path = "api/2.0/users";
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(path);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        if (response.IsSuccessStatusCode)
        {
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(contentString);

            var users = JsonConvert.DeserializeObject<Content<IEnumerable<UserReadDto>>>(contentString);

            // Assert
            Assert.Equal(4, users.Data.Count());
        }
        else
        {
            var errorString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(errorString); // Add this line to verify that the error string is not null

            var error = JsonConvert.DeserializeObject<Object>(errorString);

            Assert.Null(error); // You should not get an error if the status code was OK
        }
    }

    [Fact]
    public async Task GetUser_ShouldReturnOk_WhenUserExists()
    {
        const string philipId = "123e4567-e89b-12d3-a456-426655440000";
        const string path = $"api/2.0/users/{philipId}";
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(path);



        if (response.IsSuccessStatusCode)
        {
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(contentString);

            var user = JsonConvert.DeserializeObject<Content<UserReadDto>>(contentString);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Philip", user.Data.FirstName);
            Assert.Equal("philip.eiler@hotmail.com", user.Data.UserEmail);
        }
        else
        {
            var errorString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(errorString);
            var error = JsonConvert.DeserializeObject<Error>(errorString);

            Assert.Null(error);
        }
    }

    [Fact]
    public async Task GetUser_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        const string path = "api/2.0/users/100";
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(path);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        if (response.IsSuccessStatusCode)
        {
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(contentString);

            var user = JsonConvert.DeserializeObject<Content<UserReadDto>>(contentString);

            Assert.Null(user);
        }
        else
        {
            var errorString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(errorString);
            var error = JsonConvert.DeserializeObject<Error>(errorString);

            Assert.False(error.Status);
            Assert.Equal("User not exist", error.ErrorMessage);
        }
    }

    [Fact]
    public async Task UpdateUserGenrePreference_ShouldReturnOk_WhenUserExists()
    {
        const string philipId = "123e4567-e89b-12d3-a456-426655440000";
        var client = _factory.CreateClient();
        const string path = $"api/2.0/users/{philipId}/config/genrePreference";
        var preferences = new Dictionary<string, bool>
        {
            { "Action", true },
            { "Drama", false }
        };

        var json = JsonConvert.SerializeObject(preferences);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        // Act
        var response = await client.PostAsync(path, content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        if (response.IsSuccessStatusCode)
        {
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(contentString);

            var result = JsonConvert.DeserializeObject<Content<string>>(contentString);

            Assert.Equal("user genre preferences updated", result.Data);
        }
        else
        {
            var errorString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(errorString);
            var error = JsonConvert.DeserializeObject<Error>(errorString);

            Assert.False(error.Status);
            Assert.Equal("User not exist", error.ErrorMessage);
        }
    }

    [Fact]
    public async Task UpdateUserGenrePreference_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        const string path = "api/2.0/users/100/config/genrePreference";
        var preferences = new Dictionary<string, bool>
    {
        { "Action", true },
        { "Drama", false }
    };

        // Arrange
        var client = _factory.CreateClient();
        var json = JsonConvert.SerializeObject(preferences);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(path, content);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        if (response.IsSuccessStatusCode)
        {
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(contentString);

            var result = JsonConvert.DeserializeObject<Content<string>>(contentString);

            Assert.Null(result);
        }
        else
        {
            var errorString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(errorString);
            var error = JsonConvert.DeserializeObject<Error>(errorString);

            Assert.False(error.Status);
            Assert.Equal("User not exist", error.ErrorMessage);
        }
    }

    [Fact]
    public async Task CreateUser_ShouldReturnOk_WhenValidUserSignUpDto()
    {
        const string path = "api/2.0/users";
        var userSignUpDto = new UserSignUpDto
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@example.com",
            Password = "password",
            GenreConfig = new Dictionary<string, bool>
        {
            { "Comedy", true },
            { "Romance", false }
        }
        };

        // Arrange
        var client = _factory.CreateClient();
        var json = JsonConvert.SerializeObject(userSignUpDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(path, content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        if (response.IsSuccessStatusCode)
        {
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(contentString);

            var result = JsonConvert.DeserializeObject<Content<UserSignUpDto>>(contentString);

            Assert.Equal(userSignUpDto.FirstName, result.Data.FirstName);
            Assert.Equal(userSignUpDto.LastName, result.Data.LastName);
            Assert.Equal(userSignUpDto.Email, result.Data.Email);
        }
        else
        {
            var errorString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(errorString);
            var error = JsonConvert.DeserializeObject<Error>(errorString);

            Assert.False(error.Status);
            Assert.Equal("something was wrong", error.ErrorMessage);
        }
    }
    [Fact]
    public async Task CreateUser_ShouldReturnBadRequest_WhenInvalidUserSignUpDto()
    {
        const string path = "api/2.0/users";
        var userSignUpDto = new UserSignUpDto
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@example.com",
            Password = "password",
            GenreConfig = new Dictionary<string, bool>()
        };
        // Arrange
        var client = _factory.CreateClient();
        var json = JsonConvert.SerializeObject(userSignUpDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(path, content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        if (response.IsSuccessStatusCode)
        {
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(contentString);

            var result = JsonConvert.DeserializeObject<Content<UserSignUpDto>>(contentString);

            Assert.Null(result);
        }
        else
        {
            var errorString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(errorString);
            var error = JsonConvert.DeserializeObject<Error>(errorString);

            Assert.False(error.Status);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }

}