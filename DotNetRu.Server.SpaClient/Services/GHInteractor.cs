using System.Threading.Tasks;
using Octokit;

namespace DotNetRu.Server.SpaClient.Services
{
    public class GHInteractor
    {
        public async Task<string> TryGetRepoData(string username, string token)
        {
            var basicAuth = new Credentials(username, token);
            var client = new GitHubClient(new ProductHeaderValue("TestGithubApp"))
            {
                Credentials = basicAuth
            };
            var repo = await client.Repository.Get("zetroot", "zetroot");
            return $"repo name:{repo.Name}, stars: {repo.StargazersCount}";
        }
    }
}
