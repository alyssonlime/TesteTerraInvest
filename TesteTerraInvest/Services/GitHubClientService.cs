using Octokit;
using Octokit.Internal;
using TesteTerraInvest.Interfaces;

namespace TesteTerraInvest.Services
{
    public class GitHubClientService : GitHubClient, IGitHubClientService
    {
        public GitHubClientService(ProductHeaderValue productInformation) : base(productInformation) { }

        public GitHubClientService(IConnection connection) : base(connection) { }

        public GitHubClientService(ProductHeaderValue productInformation, ICredentialStore credentialStore) : base(productInformation, credentialStore) { }

        public GitHubClientService(ProductHeaderValue productInformation, Uri baseAddress) : base(productInformation, baseAddress) { }

        public GitHubClientService(ProductHeaderValue productInformation, ICredentialStore credentialStore, Uri baseAddress) : base(productInformation, credentialStore, baseAddress) { }

        public void SetCredentials(HttpRequest httpRequest)
        {
            if (httpRequest != null && httpRequest.Headers.Any(p => p.Key == "Authorization"))
            {
                var token = httpRequest.Headers.FirstOrDefault(p => p.Key == "Authorization").Value.ToString();
                
                if (token.ToUpper().StartsWith("BEARER "))
                {
                    token = token.Substring(7, token.Length - 7);
                }

                SetCredentials(token);
            }
        }

        public void SetCredentials(string token)
        {
            Connection.Credentials = new Credentials(token);
        }
    }
}
