using Octokit;
using Octokit.Internal;

namespace DotNetRu.Commune.GithubFileSystem
{
    /// <summary>
    /// Factoy for building clients
    /// </summary>
    public class ClientFactory
    {
        private const string ProductName = "DotNetRuCommune";

        /// <summary>
        /// Build anonymous client - no credentials needed
        /// </summary>
        /// <returns>Github client with anonymous credentials</returns>
        public GitHubClient Anonymous()
        {
            var credStore = new InMemoryCredentialStore(Credentials.Anonymous);
            var client = new GitHubClient(new Connection(new ProductHeaderValue(ProductName),
                GitHubClient.GitHubApiUrl, credStore,
                new HttpClientAdapter(Net5HttpMessageHandlerFactory.CreateDefault),
                new SimpleJsonSerializer()));
            return client;
        }

        /// <summary>
        /// Get an authenticated client with specific token
        /// </summary>
        /// <param name="token">personal access token for github</param>
        /// <returns>GitHUb client with injeccted personal access token</returns>
        public GitHubClient WithToken(string token)
        {
            var credStore = new InMemoryCredentialStore(new (token, AuthenticationType.Bearer));
            var client = new GitHubClient(new Connection(new ProductHeaderValue(ProductName),
            GitHubClient.GitHubApiUrl, credStore,
            new HttpClientAdapter(Net5HttpMessageHandlerFactory.CreateDefault),
            new SimpleJsonSerializer()));
            return client;
        }
    }
}
