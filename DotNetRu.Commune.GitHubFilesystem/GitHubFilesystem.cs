using System;
using System.IO.Abstractions;

namespace DotNetRu.GitHubFilesystem
{
    public class GitHubFilesystem : IFileSystem
    {
        public IFile File { get; }
        public IDirectory Directory => throw new NotSupportedException();
        public IFileInfoFactory FileInfo => throw new NotSupportedException();
        public IFileStreamFactory FileStream => throw new NotSupportedException();
        public IPath Path => throw new NotSupportedException();
        public IDirectoryInfoFactory DirectoryInfo => throw new NotSupportedException();
        public IDriveInfoFactory DriveInfo => throw new NotSupportedException();
        public IFileSystemWatcherFactory FileSystemWatcher => throw new NotSupportedException();


    }
}
