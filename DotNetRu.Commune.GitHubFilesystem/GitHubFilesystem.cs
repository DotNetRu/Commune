using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Octokit;
using Octokit.Helpers;
using Octokit.Internal;

namespace DotNetRu.Commune.GitHubFilesystem
{
    public class GitHubFilesystem
    {
        private List<GitHubFileStream> files = new List<GitHubFileStream>();

        public async Task StartContext(string token, string originRepo, string originOwner)
        {
            var credStore = new InMemoryCredentialStore(new(token, AuthenticationType.Bearer));
            var client = new GitHubClient(new Connection(new ProductHeaderValue("BlazorClientApp"),
                GitHubClient.GitHubApiUrl, credStore,
                new HttpClientAdapter(Net5HttpMessageHandlerFactory.CreateDefault), new SimpleJsonSerializer()));
            var originalRepo = await client.Repository.Get(originOwner, originRepo);
            var fork = await client.Repository.Forks.Create(originalRepo.Id, new ());
            var currentBranch = await client.Git.Reference.CreateBranch(fork.Owner.Login, fork.Name, "new-branch-1");
        }

    }
}
