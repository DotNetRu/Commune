using System;
using System.Threading.Tasks;
using DotNetRu.Commune.GitHubFilesystem;
using Moq;
using Octokit;
using Xunit;

namespace DotNetRu.Commune.Test.GitHubFilesystem
{
    public class GithubFileStreamTests
    {
        private class EditingContextStub : EditingContext
        {
            private readonly IRepositoryContentsClient repoContentsClient;
            public EditingContextStub(IGitHubClient client = null,
                Repository originRepo = null,
                Reference originBranch = null,
                Repository localRepo = null,
                Reference currentBranch = null)
                : base(client ?? Mock.Of<IGitHubClient>(),
                    originRepo ?? new(),
                    originBranch ?? new(),
                    localRepo ?? new(),
                    currentBranch ?? new())
            {
            }

            override
        }
        [Fact]
        public async Task FlushAsync_WhenCalled_SavesFileContents()
        {
            //arrange
            var ghClientMock = new Mock<IGitHubClient>();
            ghClientMock.SetupGet()
            var editingContext = new EditingContext()

            var sut = new GitHubFileStream("path", );
        }
    }
}
