using System;
using System.Threading.Tasks;
using Octokit;

namespace DotNetRu.Commune.GithubFilesystem
{
    /// <summary>
    /// Represents a single session of work with filesystem. Actually - main part of it is a link to current branch of forked repository.
    /// </summary>
    internal class EditingContext
    {
        /// <summary>
        /// GitHub API client instance
        /// </summary>
        public IGitHubClient Client { get; }

        /// <summary>
        /// Sub-client for accessing API for manipulation with repository contents
        /// </summary>
        public virtual IRepositoryContentsClient ContentClient => Client.Repository.Content;

        /// <summary>
        /// Sub-client for accessing API for manipulation with pull requests
        /// </summary>
        public virtual IPullRequestsClient PullRequestsClient => Client.PullRequest;

        /// <summary>
        /// Original repository
        /// </summary>
        public Repository OriginRepo { get; }

        /// <summary>
        /// Original (source) branch
        /// </summary>
        /// <remarks>HINT: It is "master"</remarks>
        public Reference OriginBranch { get; }

        /// <summary>
        /// Users forl of original repo
        /// </summary>
        public Repository LocalRepo { get; }

        /// <summary>
        /// Current branch. All files are editing in it.
        /// </summary>
        public Reference CurrentBranch { get; }

        /// <summary>
        /// Commit the changes.
        /// Creates a pr from <see cref="CurrentBranch"/> of <see cref="LocalRepo"/> to <see cref="OriginBranch"/> of <see cref="OriginRepo"/>
        /// </summary>
        public Task Commit()
        {
            var head = $"{LocalRepo.Owner.Login}:{CurrentBranch.Ref}";
            return PullRequestsClient.Create(OriginRepo.Id,
                new("AUTOMATED PR", head, OriginBranch.Ref) {Draft = true});
        }

        public EditingContext(IGitHubClient client, Repository originRepo, Reference originBranch, Repository localRepo,
            Reference currentBranch)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            OriginRepo = originRepo ?? throw new ArgumentNullException(nameof(originRepo));
            OriginBranch = originBranch ?? throw new ArgumentNullException(nameof(originBranch));
            LocalRepo = localRepo ?? throw new ArgumentNullException(nameof(localRepo));
            CurrentBranch = currentBranch ?? throw new ArgumentNullException(nameof(currentBranch));
        }
    }
}
