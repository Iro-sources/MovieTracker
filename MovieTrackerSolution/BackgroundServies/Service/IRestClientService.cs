using RestSharp;

namespace BackgroundServies.Api.Service
{
    public interface IRestClientService
    {

        Task<RestResponse> Get(string endpoint);
    }
}
