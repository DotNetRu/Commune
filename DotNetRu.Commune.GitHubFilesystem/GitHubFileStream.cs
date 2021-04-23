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
    public class GitHubFileStream : MemoryStream
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

        public GitHubFileStream(string path, EditingContext context, string originSha, byte[] originContent) :
            base(originContent?.Length ?? throw new ArgumentNullException(nameof(originContent)))
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Context = context ?? throw new ArgumentNullException(nameof(context));
            RepoFileSha = originSha ?? throw new ArgumentNullException(nameof(originSha));

            var allocatedBuffer = base.GetBuffer();
            Buffer.BlockCopy(originContent, 0, allocatedBuffer, 0, originContent.Length);
        }

        /// <inheritdoc />
        public override void Flush()
        {
            FlushAsync(CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <inheritdoc />
        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            var content = Convert.ToBase64String(this.GetBuffer());
            var updateRequest = new UpdateFileRequest($"Flush @ {DateTime.Now}", content, RepoFileSha, Context.CurrentBranch.Ref, false);
            var updateResult = await Context.ContentClient.UpdateFile(Context.LocalRepo.Id, Path, updateRequest);
            RepoFileSha = updateResult.Content.Sha;
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            base.Write(buffer, offset, count);
            Flush();
        }

        /// <inheritdoc />
        public override void Write(ReadOnlySpan<byte> source)
        {
            base.Write(source);
            Flush();
        }

        /// <inheritdoc />
        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await base.WriteAsync(buffer, offset, count, cancellationToken);
            await FlushAsync(cancellationToken);
        }

        /// <inheritdoc />
        public override async ValueTask WriteAsync(ReadOnlyMemory<byte> source, CancellationToken cancellationToken = new CancellationToken())
        {
            await base.WriteAsync(source, cancellationToken);
            await FlushAsync(cancellationToken);
        }

        /// <inheritdoc />
        public override void WriteByte(byte value)
        {
            base.WriteByte(value);
            Flush();
        }
    }
}
