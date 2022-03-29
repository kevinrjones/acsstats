using AcsDto.Models;
using Microsoft.Extensions.Logging;
using Services.Models;

namespace Services.Remote
{
    public class BaseRemoteService
    {
        protected readonly ILogger<BaseRemoteService> Logger;

        public BaseRemoteService(ILogger<BaseRemoteService> logger)
        {
            Logger = logger;
        }
        protected string BuildUrl(SharedModel sharedServiceModel, string endpoint)
        {
            var url =
                $"{endpoint}/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";

            Logger.LogDebug("Remote URL is: {Url}", url);
            
            return url;
        }
    }
}