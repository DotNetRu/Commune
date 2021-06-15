using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetRu.Auditor.Storage.FileSystem
{
    /// <remarks>
    /// In fact, most operating systems do not support asynchronous enumeration of file system contents.
    /// This is why there is no asynchronous API in the official BCL. But we have a Blazor consumer that
    /// throws an exception when using synchronization operations, and a GutHub HTTP API implementation
    /// that can perform real asynchronous operations.
    ///
    /// To respect I/O operations, we decided to use asynchronous abstractions. But we assume that real
    /// asynchronous operations will be rare, so we decided to use ValueTask.
    /// </remarks>>
    public interface IDirectory : IFileSystemEntry
    {
        ValueTask<IDirectory> GetDirectoryInfoAsync(string subPath);

        ValueTask<IFile> GetFileInfoAsync(string subPath);

        IAsyncEnumerable<IDirectory> EnumerateDirectoriesAsync();

        IAsyncEnumerable<IFile> EnumerateFilesAsync();

        ValueTask<IFile> CreateFileAsync(string subPath);

        ValueTask DeleteFileAsync(string subPath);
    }
}
