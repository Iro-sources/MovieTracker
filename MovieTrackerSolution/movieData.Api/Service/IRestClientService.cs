using RestSharp;

namespace MovieData.Api.Service
{
    public interface IRestClientService
    {

        Task<RestResponse> Get(string endpoint);
    }
}
