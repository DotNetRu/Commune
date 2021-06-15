using System.IO;
using System.Threading.Tasks;

namespace DotNetRu.Auditor.Storage.FileSystem
{
    /// <summary>
    ///
    /// </summary>
    public interface IFile : IFileSystemEntry
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        ValueTask<Stream> OpenForReadAsync();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        ValueTask<Stream> OpenForWriteAsync();
    }
}
