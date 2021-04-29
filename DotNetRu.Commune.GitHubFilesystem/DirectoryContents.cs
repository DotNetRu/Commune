using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;

namespace DotNetRu.Commune.GitHubFilesystem
{
    public class DirectoryContents : IDirectoryContents
    {
        private readonly IReadOnlyCollection<IFileInfo> files;

        public DirectoryContents(IEnumerable<IFileInfo> files)
        {
            this.files = files?.ToList() ?? throw new ArgumentNullException(nameof(files));
        }

        public IEnumerator<IFileInfo> GetEnumerator() => files.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Exists => true;
    }
}
