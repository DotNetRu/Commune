namespace DotNetRu.Commune.Test.Fs.GitHubFilesystem

open System
open DotNetRu.Commune.GitHubFilesystem
open Moq
open Octokit
open Xunit

type private EditingContextStub(repoContentsClient : IRepositoryContentsClient, client : IGitHubClient, originRepo : Repository, originBranch : Reference, localRepo : Repository, currentBranch : Reference) =
        inherit EditingContext(client, originRepo, originBranch, localRepo, currentBranch)
        let contentsClient = repoContentsClient
        new(repoContentsClient : IRepositoryContentsClient) = EditingContextStub(repoContentsClient, Mock.Of<IGitHubClient>(), Repository(), Reference(), Repository(), Reference())
        override this.ContentClient
            with get() = contentsClient

type GitHubFileStreamTests() =

    [<Fact>]
    let ``Ctor fails on null path`` () =
        let editingContext = EditingContext(Mock.Of<IGitHubClient>(), Repository(), Reference(), Repository(), Reference())
        Assert.Throws<ArgumentNullException>(fun () -> new GitHubFileStream(null, editingContext, String.Empty, array.Empty<byte>()) |> ignore)

    [<Fact>]
    let ``Ctor fails on null context`` () =
        Assert.Throws<ArgumentNullException>(fun () -> new GitHubFileStream("path", null, String.Empty, array.Empty<byte>()) |> ignore)

    [<Fact>]
    let ``Ctor fails on null sha`` () =
        let editingContext = EditingContext(Mock.Of<IGitHubClient>(), Repository(), Reference(), Repository(), Reference())
        Assert.Throws<ArgumentNullException>(fun () -> new GitHubFileStream("path", editingContext, null, array.Empty<byte>()) |> ignore);

    [<Fact>]
    let ``Ctor fails on null data array`` () =
        let editingContext = EditingContext(Mock.Of<IGitHubClient>(), Repository(), Reference(), Repository(), Reference())
        Assert.Throws<ArgumentNullException>(fun () -> new GitHubFileStream("path", editingContext, "sha", null) |> ignore);

    [<Fact>]
    let ``FlushAsync invokes content client from context to save data`` () =
        async {
            //arrange
            let repoContentMock = Mock<IRepositoryContentsClient>()
            let repoContentInfo = RepositoryContentInfo("", "", "NEW SHA", 0, ContentType.File, "", "", "", "")
            let changeSet = RepositoryContentChangeSet(repoContentInfo, Commit())
            repoContentMock.Setup(fun(x) -> x.UpdateFile(It.IsAny<int64>(), It.IsAny<string>(), It.IsAny<UpdateFileRequest>())).ReturnsAsync(changeSet) |> ignore

            let contextMock = new EditingContextStub(repoContentMock.Object)
            let sut = new GitHubFileStream("", contextMock, "ORIGIN SHA", array.Empty<byte>())

            //act
            sut.FlushAsync() |> Async.AwaitTask |> ignore

            //assert
            repoContentMock.Verify(fun(x) -> )
        }

