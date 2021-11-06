using System.IO;
using System.Threading.Tasks;
using DotNetRu.Auditor.Storage.FileSystem;
using Octokit;

namespace DotNetRu.Commune.GithubFileSystem
{
    /// <summary>
    /// Files from github repository
    /// </summary>
    public class GithubFile : GithubFilesystemEntry, IFile
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gitHubClient">github client</param>
        /// <param name="repository">github repository</param>
        /// <param name="branch">branch in this repository</param>
        /// <param name="name">file name</param>
        /// <param name="fullName">file full path in repository</param>
        public GithubFile(IGitHubClient gitHubClient, Repository repository, Reference branch, string name, string fullName) :
            base(gitHubClient, repository, branch, name, fullName)
        {
        }
        /// <inheritdoc />
        public override ValueTask<bool> ExistsAsync() => throw new System.NotImplementedException();

        /// <inheritdoc />
        public Task<Stream> OpenForReadAsync() => throw new System.NotImplementedException();

        /// <inheritdoc />
        public Task<IWritableFile?> RequestWriteAccessAsync() => throw new System.NotImplementedException();

    }
}
