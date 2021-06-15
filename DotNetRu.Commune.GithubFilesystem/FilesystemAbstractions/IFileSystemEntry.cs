namespace DotNetRu.Auditor.Storage.FileSystem
{
    public interface IFileSystemEntry
    {
        string Name { get; }

        string FullName { get; }

        bool Exists { get; }
    }
}
