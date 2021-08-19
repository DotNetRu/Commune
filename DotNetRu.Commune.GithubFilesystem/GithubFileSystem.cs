using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRu.Auditor.Storage.FileSystem;
using Octokit;
using Octokit.Helpers;
using Octokit.Internal;

namespace DotNetRu.Commune.GithubFileSystem
{
    /// <summary>
    /// Virtual filesystem implementation using GitHub repositories for storing files. Implements <see cref="IFileSystem"/>
    /// </summary>
    public class GithubFileSystem : IFileSystem
    {
        private const string ProductName = "DotNetRuCommune";
        private EditingContext? _editingContext;

        /// <summary>
        /// Begin work with filesytem. Creates a fork of original repository, then creates a new branch in this fork and
        /// stores all the data inside editing context. Without calling this method you can't do anything else - enumerate, read, create
        /// </summary>
        /// <param name="token">Personal access token</param>
        /// <param name="originRepo">Original repository name</param>
        /// <param name="originOwner">Original repository owner (user or organization)</param>
        public async Task StartContext(string token, string originRepo, string originOwner)
        {
            var credStore = new InMemoryCredentialStore(new(token, AuthenticationType.Bearer));
            var client = new GitHubClient(new Connection(new ProductHeaderValue(ProductName),
                GitHubClient.GitHubApiUrl, credStore,
                new HttpClientAdapter(Net5HttpMessageHandlerFactory.CreateDefault),
                new SimpleJsonSerializer()));

            var originalRepo = await client.Repository.Get(originOwner, originRepo).ConfigureAwait(false);
            var originalBranch = await client.Git.Reference.Get(originalRepo.Id, "heads/master").ConfigureAwait(false);
            var fork = await client.Repository.Forks.Create(originalRepo.Id, new ()).ConfigureAwait(false);
            var currentBranch = await client.Git.Reference.CreateBranch(fork.Owner.Login, fork.Name, Guid.NewGuid().ToString("N")).ConfigureAwait(false);
            _editingContext = new EditingContext(client, originalRepo, originalBranch, fork, currentBranch);
        }

        /// <inheritdoc />
        public string Name => "/";

        /// <inheritdoc />
        public string FullName => "/";

        /// <inheritdoc />
        public bool Exists => true;

        /// <inheritdoc />
        public ValueTask<IDirectory> GetDirectoryInfoAsync(string subPath) => throw new System.NotImplementedException();

        /// <inheritdoc />
        public ValueTask<IFile> GetFileInfoAsync(string subPath) => throw new System.NotImplementedException();

        /// <inheritdoc />
        public IAsyncEnumerable<IDirectory> EnumerateDirectoriesAsync() => throw new System.NotImplementedException();

        /// <inheritdoc />
        public IAsyncEnumerable<IFile> EnumerateFilesAsync() => throw new System.NotImplementedException();

        /// <inheritdoc />
        public ValueTask<IFile> CreateFileAsync(string subPath) => throw new System.NotImplementedException();

        /// <inheritdoc />
        public ValueTask DeleteFileAsync(string subPath) => throw new System.NotImplementedException();
    }
}
