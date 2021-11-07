using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetRu.Auditor.Storage.FileSystem;
using Octokit;

namespace DotNetRu.Commune.GithubFileSystem
{
    /// <summary>
    /// Files from github repository
    /// </summary>
    public class GitHubFile : GitHubFilesystemEntry, IFile
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gitHubClient">github client</param>
        /// <param name="repository">github repository</param>
        /// <param name="branch">branch in this repository</param>
        /// <param name="name">file name</param>
        /// <param name="fullName">file full path in repository</param>
        public GitHubFile(IGitHubClient gitHubClient, Repository repository, Reference branch, string name, string fullName) :
            base(gitHubClient, repository, branch, name, fullName)
        {
        }

        /// <inheritdoc />
        public override async ValueTask<bool> ExistsAsync()
        {
            var parentDirectory = GetParentDirectory();
            var contents = await ContentsClient.GetAllContentsByRef(Repository.Id, parentDirectory, Branch.Ref)
                .ConfigureAwait(false);

            return contents.Any(x => x.Name == Name && x.Type.Value == ContentType.File);
        }

        /// <inheritdoc />
        public async Task<Stream> OpenForReadAsync()
        {
            var contents = await ContentsClient
                .GetRawContentByRef(Repository.Owner.Login, Repository.Name, FullName, Branch.Ref)
                .ConfigureAwait(false);
            return new MemoryStream(contents, false);
        }

        /// <inheritdoc />
        public Task<IWritableFile?> RequestWriteAccessAsync() => throw new System.NotImplementedException();

    }
}
