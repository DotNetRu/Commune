using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Octokit;

namespace DotNetRu.Commune.GitHubFilesystem
{
    /// <summary>
    /// реализация абстракции файла, хранящегося в репозитории github
    /// </summary>
    public class GithubFile : MemoryStream, IFileInfo
    {
        private readonly EditingContext _context;

        /// <summary>
        /// SHA файла в репозитории
        /// </summary>
        public string RepoFileSha { get; private set; }

        /// <summary>
        /// Содержимое файла в стриме. Можно чмитать и писать. Не забывайте делать flush
        /// </summary>
        /// <returns>The file stream</returns>
        public Stream CreateReadStream()
        {
            throw new NotSupportedException();
        }

        public bool ContentsLoaded { get; private set; }
        /// <inheritdoc />
        public bool Exists { get; private set; }

        /// <inheritdoc />
        public string PhysicalPath { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public DateTimeOffset LastModified { get; }

        /// <inheritdoc />
        public bool IsDirectory { get; }

        /// <inheritdoc />
        public override void Flush()
        {
            throw new NotSupportedException();
        }

        //public override long Length => -1;

        /// <inheritdoc />
        public override Task FlushAsync(CancellationToken cancellationToken) => FlushInternal();

        protected virtual async Task FlushInternal()
        {
            var content = Convert.ToBase64String(this.ToArray());
            if (Exists)
            {
                var updateRequest = new UpdateFileRequest($"Updated {PhysicalPath} @ {DateTime.Now}", content, RepoFileSha,
                    _context.CurrentBranch.Ref, false);
                var updateResult =
                    await _context.ContentClient.UpdateFile(_context.LocalRepo.Id, PhysicalPath, updateRequest);
                RepoFileSha = updateResult.Content.Sha;
            }
            else
            {
                var createRequest = new CreateFileRequest($"Created {PhysicalPath} @ {DateTime.Now}", content,
                    _context.CurrentBranch.Ref, false);
                var changeSet = await _context.ContentClient.CreateFile(_context.LocalRepo.Id, PhysicalPath, createRequest);
                RepoFileSha = changeSet.Content.Sha;
                Exists = true;
            }
        }

        /// <summary>
        /// Констурктор файла
        /// </summary>
        /// <param name="context">Контекст редактирования</param>
        /// <param name="length">размер файла</param>
        /// <param name="physicalPath">путь к файлу в файловой системе</param>
        /// <param name="name">имя файла</param>
        /// <param name="isDirectory">если истина - то это папка</param>
        /// <exception cref="ArgumentNullException">выбрасывается если в аргументах преедан null</exception>
        internal GithubFile([NotNull] EditingContext context,
            [NotNull] string physicalPath,
            [NotNull] string name,
            bool isDirectory, bool isNew, string originalSha, bool isLoaded) : base()
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            PhysicalPath = physicalPath ?? throw new ArgumentNullException(nameof(physicalPath));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsDirectory = isDirectory;
            Exists = !isNew;
            RepoFileSha = originalSha ?? throw new ArgumentNullException(nameof(originalSha));
            ContentsLoaded = isLoaded;
        }

        internal static async Task<GithubFile> Open([NotNull] EditingContext context,
            [NotNull] string physicalPath)
        {
            var contents = await context.ContentClient
                .GetAllContentsByRef(context.LocalRepo.Id, physicalPath, context.CurrentBranch.Ref).ConfigureAwait(false);
            var content = contents.Single();
            var fs = new GithubFile(context, content.Path, content.Name, false, false, content.Sha, true);

            var originalContent = await context.ContentClient
                .GetRawContentByRef(context.LocalRepo.Owner.Login, context.LocalRepo.Name, physicalPath, context.CurrentBranch.Ref);
            fs.Write(originalContent);
            fs.Position = 0;
            return fs;
        }

        internal static Task<GithubFile> Create([NotNull] EditingContext context,
            [NotNull] string physicalPath)
        {
            var fs = new GithubFile(context, physicalPath, physicalPath, false, true, string.Empty, true);
            fs.Position = 0;
            return Task.FromResult(fs);
        }
    }
}
