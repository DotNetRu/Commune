using System.IO;
using System.Threading.Tasks;

namespace DotNetRu.Auditor.Storage.FileSystem
{
    public interface IFile : IFileSystemEntry
    {
        ValueTask<Stream> OpenForReadAsync();

        ValueTask<Stream> OpenForWriteAsync();
    }
}
