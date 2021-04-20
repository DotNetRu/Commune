using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Octokit;
using Octokit.Helpers;
using Octokit.Internal;

namespace DotNetRu.Server.SpaClient.Services
{
    public class AuditGithubClient
    {
        private readonly ILogger<AuditGithubClient> logger;
        private GitHubClient? client;
        private Repository? originalRepo;
        private Repository? myFork;
        private Reference? currentBranch;

        public AuditGithubClient(ILogger<AuditGithubClient> logger)
        {
            this.logger = logger;
        }

        public bool IsInitialized { get; private set; } = false;

        public void Initialize([NotNull] string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            try
            {
                var credStore = new InMemoryCredentialStore(new(token, AuthenticationType.Bearer));
                logger.LogTrace("Credentials store created");
                client = new GitHubClient(new Connection(new ProductHeaderValue("BlazorClientApp")));
                IsInitialized = true;
                logger.LogTrace("Client created");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed creating github api client");
                client = null;
                IsInitialized = false;
            }
        }

        public async Task<List<string>> ListRepositories()
        {
            try
            {
                if (client == null) throw new InvalidOperationException();
                var currentUser = await client.User.Current();
                var owner = currentUser.Login;
                var repos = await client.Repository.GetAllForUser(currentUser.Login);
                return repos.Select(x => x.Name).ToList();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed");
                throw;
            }
        }

        public async Task<string> ForkAudit()
        {
            if (client == null) throw new InvalidOperationException();
            //locate audit
            originalRepo = await client.Repository.Get("SelectFromGroup-By", "DEMO");
            myFork = await client.Repository.Forks.Create(originalRepo.Id, new NewRepositoryFork());
            return myFork.Url;
        }

        public async Task<string> NewBranchLocal()
        {
            if (client == null) throw new InvalidOperationException();
            if (myFork == null) throw new InvalidOperationException();
            logger.LogTrace("Creating branch in {User}/{Repo}", myFork.Owner.Login, myFork.Name);
            currentBranch = await client.Git.Reference.CreateBranch(myFork.Owner.Login, myFork.Name, "new-branch-1");
            return currentBranch.Url;
        }

        public async Task<string> AddNewFileAndPR()
        {
            if (client == null) throw new InvalidOperationException();
            if (myFork == null) throw new InvalidOperationException();
            if (originalRepo == null) throw new InvalidOperationException();
            if (currentBranch == null) throw new InvalidOperationException();
            var changeset = await client.Repository.Content.CreateFile(myFork.Id, "test.md",
                new CreateFileRequest("automated commit", "THIS IS  FILE CONTENT", branch: currentBranch.Ref));

            var originMaster = await client.Git.Reference.Get(originalRepo.Id, "refs/heads/master");
            logger.LogTrace("Origin master ref: {Ref}", originMaster.Ref);
            logger.LogTrace("PR src ref: {Owner:}{Ref}", myFork.Owner.Login, currentBranch.Ref);
            var pr = await client.PullRequest.Create(originalRepo.Id,
                new("AUTOMATED PR", $"{myFork.Owner.Login}:{currentBranch.Ref}", originMaster.Ref) {Draft = true});
            return pr.HtmlUrl;
        }
    }
}
