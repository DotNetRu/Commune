using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRu.Auditor.Storage.FileSystem;
using Octokit;

namespace DotNetRu.Commune.GithubFileSystem
{
    /// <summary>
    /// Virtual directory in github repository. Implements <see cref="IDirectory"/>
    /// </summary>
    public class GithubDirectory : GithubFilesystemEntry, IDirectory
    {
        private GithubDirectory(IGitHubClient gitHubClient, Repository repository, Reference branch, string name, string fullName) :
            base(gitHubClient, repository, branch, name, fullName)
        {
        }

        /// <summary>
        /// Factory method for building root directory
        /// </summary>
        /// <param name="gitHubClient">Github client for this directory, stores authentication data in it</param>
        /// <param name="repository">repository, contents are accessed to</param>
        /// <param name="branch">branch in the reposiroty, to work with</param>
        /// <returns>new directory instance pointing to the root of content of this branch in this repository</returns>
        public static IDirectory ForRoot(IGitHubClient gitHubClient, Repository repository, Reference branch)
        {
            return new GithubDirectory(gitHubClient, repository, branch, string.Empty, string.Empty);
        }

        private string GetChildFullName(string childDirectoryName) =>
            FullName switch
            {
                null => childDirectoryName,
                "" => childDirectoryName,
                {} s when s.EndsWith("/") => $"{FullName}{childDirectoryName}",
                _ => $"{FullName}/{childDirectoryName}"
            };

        /// <inheritdoc />
        public IDirectory GetDirectory(string childDirectoryName)
        {
            var childFullName = GetChildFullName(childDirectoryName);
            return new GithubDirectory(GitHubClient, Repository, Branch, childDirectoryName, childFullName);
        }

        /// <inheritdoc />
        public IFile GetFile(string childFileName) => throw new NotImplementedException();

        /// <inheritdoc />
        public async IAsyncEnumerable<IDirectory> EnumerateDirectoriesAsync()
        {
            var contents = await ContentsClient.GetAllContentsByRef(Repository.Id, FullName, Branch.Ref)
                .ConfigureAwait(false);
            foreach (var content in contents.Where(x => x.Type.Value == ContentType.Dir))
            {
                yield return new GithubDirectory(GitHubClient, Repository, Branch, content.Name, content.Path);
            }
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<IFile> EnumerateFilesAsync()
        {
            var contents = await ContentsClient.GetAllContentsByRef(Repository.Id, FullName, Branch.Ref)
                .ConfigureAwait(false);
            foreach (var content in contents.Where(x => x.Type.Value == ContentType.File))
            {
                yield return new GithubFile(GitHubClient, Repository, Branch, content.Name, content.Path);
            }
        }

        /// <inheritdoc />
        public override ValueTask<bool> ExistsAsync() => throw new NotImplementedException();
    }
}
