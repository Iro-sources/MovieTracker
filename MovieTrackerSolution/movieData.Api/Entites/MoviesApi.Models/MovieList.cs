using Newtonsoft.Json;

namespace MovieData.Api.Entites.MoviesApi.Models
{
    public class MovieList
    {
        [JsonProperty("items")]
        public List<MovieDto> Items { get; set; }
        public string errorMessage { get; set; }
    }
}