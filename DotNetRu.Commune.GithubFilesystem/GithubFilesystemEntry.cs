using System;
using System.Threading.Tasks;
using DotNetRu.Auditor.Storage.FileSystem;
using Octokit;

namespace DotNetRu.Commune.GithubFileSystem
{
    /// <summary>
    /// base class for all filesystem objects - directories and files
    /// </summary>
    public abstract class GithubFilesystemEntry : IFileSystemEntry
    {
        /// <summary>
        /// Github client, used to access github data
        /// </summary>
        protected readonly IGitHubClient GitHubClient;

        /// <summary>
        /// repository in github where data contents are stored
        /// </summary>
        protected readonly Repository Repository;

        /// <summary>
        /// repository branch wich contents are browsed or modified
        /// </summary>
        protected readonly Reference Branch;

        /// <summary>
        /// helper property to access contents client. Contents client is a wrapper over contents endpoint, it is used for manipulating data in repository
        /// </summary>
        protected IRepositoryContentsClient ContentsClient => GitHubClient.Repository.Content;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gitHubClient">github client</param>
        /// <param name="repository">github repository</param>
        /// <param name="branch">branch in repository</param>
        /// <param name="name">name of this entry</param>
        /// <param name="fullName">full name, aka path of this entry</param>
        protected GithubFilesystemEntry(IGitHubClient gitHubClient, Repository repository, Reference branch, string name, string fullName)
        {
            GitHubClient = gitHubClient;
            Repository = repository;
            Branch = branch;
            Name = name;
            FullName = fullName;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string FullName { get; }

        /// <inheritdoc />
        public abstract ValueTask<bool> ExistsAsync();

        /// <summary>
        /// Get parent directory name containing this entry
        /// </summary>
        /// <returns>path of the parent directory</returns>
        protected string GetParentDirectory() =>
            FullName.Remove(FullName.Length - Name.Length) switch
            {
                "/" => string.Empty,
                {} s => s
            };
    }
}
