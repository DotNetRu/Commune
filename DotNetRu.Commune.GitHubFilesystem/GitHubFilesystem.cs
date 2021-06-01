using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Octokit;
using Octokit.Helpers;
using Octokit.Internal;

namespace DotNetRu.Commune.GitHubFilesystem
{
    /// <summary>
    /// Провайдер файлов с github
    /// </summary>
    public class GitHubFilesystem : IFileProvider
    {
        private EditingContext? _editingContext;

        /// <summary>
        /// Начать сессию работы с файловой системой
        /// </summary>
        /// <param name="token">PAT-токен</param>
        /// <param name="originRepo">наименование исходного репозитория</param>
        /// <param name="originOwner">владелец исходного репозитория</param>
        public async Task StartContext(string token, string originRepo, string originOwner)
        {
            var credStore = new InMemoryCredentialStore(new(token, AuthenticationType.Bearer));
            var client = new GitHubClient(new Connection(new ProductHeaderValue("DotNetRuCommune"),
                GitHubClient.GitHubApiUrl, credStore,
                new HttpClientAdapter(Net5HttpMessageHandlerFactory.CreateDefault), new SimpleJsonSerializer()));

            var originalRepo = await client.Repository.Get(originOwner, originRepo).ConfigureAwait(false);
            var originalBranch = await client.Git.Reference.Get(originalRepo.Id, "heads/master").ConfigureAwait(false);
            var fork = await client.Repository.Forks.Create(originalRepo.Id, new ()).ConfigureAwait(false);
            var currentBranch = await client.Git.Reference.CreateBranch(fork.Owner.Login, fork.Name, Guid.NewGuid().ToString("N")).ConfigureAwait(false);
            _editingContext = new EditingContext(client, originalRepo, originalBranch, fork, currentBranch);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (_editingContext == null) throw new InvalidOperationException();
            var foundContents = _editingContext.ContentClient.GetAllContents(_editingContext.LocalRepo.Id, subpath)
                .GetAwaiter().GetResult();
            if (foundContents.Count == 0) throw new FileNotFoundException("No file found for given subpath", subpath);
            var content = foundContents.First(); // what shall we do if there are several contents?
            return FileFactory(content);
        }

        public async Task<IFileInfo> GetFileInfoAsync(string subpath)
        {
            if (_editingContext == null) throw new InvalidOperationException();
            var foundContents =
                await _editingContext.ContentClient.GetAllContents(_editingContext.LocalRepo.Id, subpath);
            if (foundContents.Count == 0) throw new FileNotFoundException("No file found for given subpath", subpath);
            var content = foundContents.First(); // what shall we do if there are several contents?
            return FileFactory(content);
        }

        public IDirectoryContents GetDirectoryContents(string subpath) // TODO blazor не может в GetAwaiter().GetResult(). Поэтому надо реализовать свой асинхронный IFileProvider
        {
            if (_editingContext == null) throw new InvalidOperationException();
            var foundContents = _editingContext.ContentClient.GetAllContents(_editingContext.LocalRepo.Id, subpath)
                .Result;
            return new DirectoryContents(foundContents.Select(FileFactory));
        }

        public async Task<IDirectoryContents> GetDirectoryContentsAsync(string subpath)
        {
            if (_editingContext == null) throw new InvalidOperationException();
            var foundContents = await _editingContext.ContentClient
                .GetAllContents(_editingContext.LocalRepo.Id, subpath).ConfigureAwait(false);
            return new DirectoryContents(foundContents.Select(FileFactory));
        }

        private GithubFile FileFactory(RepositoryContent content) =>
            new(_editingContext ?? throw new InvalidOperationException(),
                content.Size,
                content.Path,
                content.Name,
                content.Type.Value == ContentType.Dir);
        public IChangeToken Watch(string filter) => throw new NotSupportedException();

        /// <summary>
        /// Внести изменения создав новый PR
        /// </summary>
        /// <exception cref="InvalidOperationException">выбрасывается если не начат контекст редактирования репозитория</exception>
        public async Task CommitChanges()
        {
            if (_editingContext == null) throw new InvalidOperationException();
            try
            {
                await _editingContext.Commit().ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
