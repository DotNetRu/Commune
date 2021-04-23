namespace DotNetRu.Commune.Test.Fs.GitHubFilesystem

open System
open System.Linq.Expressions
open System.Threading
open System.Threading.Tasks
open DotNetRu.Commune.GitHubFilesystem
open Moq
open Octokit
open Xunit
open Foq
open FluentAssertions

type private EditingContextStub(repoContentsClient : IRepositoryContentsClient, client : IGitHubClient, originRepo : Repository, originBranch : Reference, localRepo : Repository, currentBranch : Reference) =
        inherit EditingContext(client, originRepo, originBranch, localRepo, currentBranch)
        let contentsClient = repoContentsClient
        new(repoContentsClient : IRepositoryContentsClient) =
            EditingContextStub(repoContentsClient, Mock.Of<IGitHubClient>(), Repository(), Reference("master","","", TagObject()), Repository(), Reference("test","","", TagObject()))
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
        Assert.Throws<ArgumentNullException>(fun () -> new GitHubFileStream("path", editingContext, "sha", null) |> ignore)

    [<Fact>]
    let ``FlushAsync invokes saves data in content client and updates SHA`` () =
        async {
            //arrange
            let repoContentInfo = RepositoryContentInfo("", "", "NEW SHA", 0, ContentType.File, "", "", "", "")
            let changeSet = RepositoryContentChangeSet(repoContentInfo, Commit())
            let repoContentMock =
                Mock<IRepositoryContentsClient>()
                    .Setup(fun(x) -> <@ x.UpdateFile(any(), any(), any()) @>)
                    .Returns(Task.FromResult(changeSet))
                    .Create()

            let contextMock = new EditingContextStub(repoContentMock)
            let path = "file path"
            let originalSha = "original sha"
            let sut = new GitHubFileStream(path, contextMock, originalSha, [|0x0Duy; 0x0Auy|])

            //act
            sut.FlushAsync() |> Async.AwaitTask |> ignore

            //assert
            verify <@ repoContentMock.UpdateFile(any(), is(fun x -> x = path), is(fun (x:UpdateFileRequest) -> x.Sha = originalSha)) @> once
        }

    [<Fact>]
    let ``Flush calls FlushAsync``() =
        // arrange
        let editingContext = EditingContext(Mock.Of<IGitHubClient>(), Repository(), Reference(), Repository(), Reference())
        let flushAsyncInvocationsCnt = ref 0
        let sut = {
            new GitHubFileStream("", editingContext, "", array.Empty()) with
                override x.FlushAsync(token : CancellationToken) =
                    incr flushAsyncInvocationsCnt
                    Task.CompletedTask
            }

        //act
        sut.Flush()

        //assert
        flushAsyncInvocationsCnt.contents.Should().Be(1, "")



