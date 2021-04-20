using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Octokit;
using Octokit.Helpers;
using Octokit.Internal;

namespace DotNetRu.Commune.GitHubFilesystem
{
    /// <summary>
    /// Провайдер файлов с github
    /// </summary>
    public class GitHubFilesystem
    {
        private List<GitHubFileStream> files = new List<GitHubFileStream>();

        /// <summary>
        /// Начать сессию работы с файловой системой
        /// </summary>
        /// <param name="token">PAT-токен</param>
        /// <param name="originRepo">наименование исходного репозитория</param>
        /// <param name="originOwner">владелец исходного репозитория</param>
        public async Task StartContext(string token, string originRepo, string originOwner)
        {
            var credStore = new InMemoryCredentialStore(new(token, AuthenticationType.Bearer));
            var client = new GitHubClient(new Connection(new ProductHeaderValue("DotNetRu.Commune.GitHubFilesystem"),
                GitHubClient.GitHubApiUrl, credStore,
                new HttpClientAdapter(Net5HttpMessageHandlerFactory.CreateDefault), new SimpleJsonSerializer()));
            var originalRepo = await client.Repository.Get(originOwner, originRepo);
            var fork = await client.Repository.Forks.Create(originalRepo.Id, new ());
            var currentBranch = await client.Git.Reference.CreateBranch(fork.Owner.Login, fork.Name, Guid.NewGuid().ToString("N"));
        }

    }
}
