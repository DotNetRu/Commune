using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Octokit;

namespace DotNetRu.Commune.GitHubFilesystem
{
    /// <summary>
    /// Стрим данных в памяти, связанный с файлом в github репозитории
    /// </summary>
    internal class GitHubFileStream : MemoryStream
    {
        /// <summary>
        /// Путь к файлу в репозитории
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// SHA файла в репозитории
        /// </summary>
        public string RepoFileSha { get; private set; }

        /// <summary>
        /// Контекст в котором существует файл
        /// </summary>
        public EditingContext Context { get; }

        public GitHubFileStream(string path, EditingContext context, string originSha)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Context = context ?? throw new ArgumentNullException(nameof(context));
            RepoFileSha = originSha ?? throw new ArgumentNullException(nameof(originSha));
        }

        /// <inheritdoc />
        public override void Flush()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override Task FlushAsync(CancellationToken cancellationToken) => FlushInternal();

        protected virtual async Task FlushInternal()
        {
            var content = Convert.ToBase64String(this.ToArray());
            var updateRequest = new UpdateFileRequest($"Flush @ {DateTime.Now}", content, RepoFileSha, Context.CurrentBranch.Ref, false);
            var updateResult = await Context.ContentClient.UpdateFile(Context.LocalRepo.Id, Path, updateRequest);
            RepoFileSha = updateResult.Content.Sha;
        }
    }
}
