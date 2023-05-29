using Octokit;

namespace TesteTerraInvest.Interfaces
{
    public interface IGitHubClientService: IGitHubClient
    {
        void SetCredentials(HttpRequest httpRequest);
        void SetCredentials(string token);
    }
}
