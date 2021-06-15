namespace DotNetRu.Auditor.Storage.FileSystem
{
    /// <summary>
    ///
    /// </summary>
    public interface IFileSystemEntry
    {
        /// <summary>
        ///
        /// </summary>
        string Name { get; }

        /// <summary>
        ///
        /// </summary>
        string FullName { get; }

        /// <summary>
        ///
        /// </summary>
        bool Exists { get; }
    }
}
