using RestSharp;

namespace AuthGateway.Api.Services
{
    public class RestClientService : IRestClientService
    {

        private readonly RestClient _restClient;
        private readonly string _url = "https://localhost:7114"; // not optimal but will do for now.

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

        public async Task<RestResponse> Post(string endpoint, object obj)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(obj);
            var response = await _restClient.ExecuteAsync(request);
            return response;
        }
    }
}

