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
        /// <summary>
        ///
        /// </summary>
        /// <param name="subPath"></param>
        /// <returns></returns>
        ValueTask<IDirectory> GetDirectoryInfoAsync(string subPath);

        /// <summary>
        ///
        /// </summary>
        /// <param name="subPath"></param>
        /// <returns></returns>
        ValueTask<IFile> GetFileInfoAsync(string subPath);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<IDirectory> EnumerateDirectoriesAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<IFile> EnumerateFilesAsync();

        /// <summary>
        ///
        /// </summary>
        /// <param name="subPath"></param>
        /// <returns></returns>
        ValueTask<IFile> CreateFileAsync(string subPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subPath"></param>
        /// <returns></returns>
        ValueTask DeleteFileAsync(string subPath);
    }
}
