using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;

namespace AcsStatsWeb.AcsHttpClient
{
    public interface IHttpClientProxy
    {
        public Task<Result<T, Error>> GetJsonAsync<T>(string uri) where T : class, new();
    }
}