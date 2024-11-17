namespace MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos
{
    public class UserReadDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string UserEmail { get; set; }
        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserGenreConfigId { get; set; }

        public List<string> SubscribedGenres { get; set; }
    }
}
