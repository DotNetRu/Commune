using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace DotNetRu.Commune.GitHubFilesystem
{
    public class GithubFile : IFileInfo
    {
        public Stream CreateReadStream() => throw new NotImplementedException();

        public bool Exists { get; }
        public long Length { get; }
        public string PhysicalPath { get; }
        public string Name { get; }
        public DateTimeOffset LastModified { get; }
        public bool IsDirectory { get; }
    }
}
