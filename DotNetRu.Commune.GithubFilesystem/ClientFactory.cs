using Octokit;
using Octokit.Internal;

namespace DotNetRu.Commune.GithubFileSystem
{
    /// <summary>
    /// Factoy for building clients
    /// </summary>
    public static class ClientFactory
    {
        private const string ProductName = "DotNetRuCommune";

        /// <summary>
        /// Build anonymous client - no credentials needed
        /// </summary>
        /// <returns></returns>
        public static GitHubClient Anonymous()
        {
            var credStore = new InMemoryCredentialStore(Credentials.Anonymous);
            var client = new GitHubClient(new Connection(new ProductHeaderValue(ProductName),
                GitHubClient.GitHubApiUrl, credStore,
                new HttpClientAdapter(Net5HttpMessageHandlerFactory.CreateDefault),
                new SimpleJsonSerializer()));
            return client;
        }
    }
}
