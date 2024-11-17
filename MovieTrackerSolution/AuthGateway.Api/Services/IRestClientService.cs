using RestSharp;

namespace AuthGateway.Api.Services
{
    public interface IRestClientService
    {

        Task<RestResponse> Get(string endpoint);

        Task<RestResponse> Post(string endpoint, object obj);
    }
}
