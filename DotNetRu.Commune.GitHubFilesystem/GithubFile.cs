using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace DotNetRu.Commune.GitHubFilesystem
{
    /// <summary>
    /// реализация абстракции файла, хранящегося в репозитории github
    /// </summary>
    public class GithubFile : IFileInfo
    {
        private readonly EditingContext _context;

        /// <summary>
        /// Содержимое файла в стриме. Можно чмитать и писать. Не забывайте делать flush
        /// </summary>
        /// <returns>The file stream</returns>
        public Stream CreateReadStream()
        {
            throw new NotSupportedException();
        }

        public async Task<Stream> CreateReadStreamAsync()
        {
            var contents = await _context.ContentClient
                .GetAllContentsByRef(_context.LocalRepo.Id, PhysicalPath, _context.CurrentBranch.Ref).ConfigureAwait(false);
            var contentHead = contents.Single();
            var fs = new GitHubFileStream(PhysicalPath, _context, contentHead.Sha);
            var originalContent = await _context.ContentClient
                .GetRawContentByRef(_context.LocalRepo.Owner.Login, _context.LocalRepo.Name, PhysicalPath, _context.CurrentBranch.Ref);
            fs.Write(originalContent);
            fs.Position = 0;
            return fs;
        
        }

        /// <inheritdoc />
        public bool Exists => true;

        /// <inheritdoc />
        public long Length { get; }

        /// <inheritdoc />
        public string PhysicalPath { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public DateTimeOffset LastModified { get; }

        /// <inheritdoc />
        public bool IsDirectory { get; }

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
            long length,
            [NotNull] string physicalPath,
            [NotNull] string name,
            bool isDirectory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Length = length;
            PhysicalPath = physicalPath ?? throw new ArgumentNullException(nameof(physicalPath));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsDirectory = isDirectory;
        }
    }
}
