using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace DotNetRu.Commune.GitHubFilesystem
{
    internal class GithubFile : IFileInfo
    {
        private readonly EditingContext _context;
        private readonly string originSha;

        /// <summary>
        /// Содержимое файла в стриме. Можно чмитать и писать. Не забывайте делать flush
        /// </summary>
        /// <returns>The file stream</returns>
        public Stream CreateReadStream()
        {
            var fs = new GitHubFileStream(PhysicalPath, _context, originSha);
            var originalContent = _context.ContentClient
                .GetRawContent(_context.LocalRepo.Owner.Login, _context.LocalRepo.Name, PhysicalPath)
                .GetAwaiter()
                .GetResult();
            fs.Write(originalContent);
            fs.Position = 0;
            return fs;
        }

        public bool Exists => true;
        public long Length { get; }
        public string PhysicalPath { get; }
        public string Name { get; }
        public DateTimeOffset LastModified { get; }
        public bool IsDirectory { get; }

        public GithubFile(EditingContext context, string originSha, long length, string physicalPath, string name,
            bool isDirectory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.originSha = originSha ?? throw new ArgumentNullException(nameof(originSha));
            Length = length;
            PhysicalPath = physicalPath ?? throw new ArgumentNullException(nameof(physicalPath));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsDirectory = isDirectory;
        }
    }
}
