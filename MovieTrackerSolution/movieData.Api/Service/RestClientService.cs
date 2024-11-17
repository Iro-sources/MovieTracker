using RestSharp;

namespace MovieData.Api.Service
{
    public class RestClientService : IRestClientService
    {
        private readonly RestClient _restClient;
        private readonly string _url = "https://imdb-api.com";

        public RestClientService()
        {
            _restClient = new RestClient(_url);
        }


        public async Task<RestResponse> Get(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _restClient.ExecuteAsync(request);
            return response;
        }
    }
}
