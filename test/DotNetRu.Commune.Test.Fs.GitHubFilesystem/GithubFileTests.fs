namespace DotNetRu.Commune.Test.Fs.GitHubFilesystem

open System
open System.IO
open System.Threading.Tasks
open DotNetRu.Commune.GitHubFilesystem
open Octokit
open Xunit
open Foq
open FluentAssertions
open MyMocks

type EditingContextContentClientMock =
    inherit EditingContext
    val repositoryContentClient : IRepositoryContentsClient

    new(repoContentClient : IRepositoryContentsClient, originRepo : Repository, originBranch : Reference, forkRepo : Repository, currentBranch : Reference) =
        {
            inherit EditingContext(Mock.Of<IGitHubClient>(), originRepo, originBranch, forkRepo, currentBranch)
            repositoryContentClient = repoContentClient
        }

    override this.ContentClient
        with get() = this.repositoryContentClient





type GithubFileTests() =

    let originOwner =
        UserMock(PubLogin = "originOwner")
    let forkOwner =
        UserMock(PubLogin = "forkOwner")

    let originRepo =
        RepositoryMock(PubName = "repoName", PubOwner = originOwner)

    let forkRepo =
        RepositoryMock(PubName = "repoName", PubOwner = forkOwner)

    let originBranch = Reference()

    let currentBranch = Reference()

    let context =
        EditingContext(Mock.Of<IGitHubClient>(), originRepo, originBranch, forkRepo, currentBranch)

    [<Fact>]
    let ``Ctor fails on null context``() =
        Assert.Throws<ArgumentNullException>(fun () -> GithubFile(null, "", 0L, "", "", false) |> ignore)

    [<Fact>]
    let ``Ctor fails on null origin sha``() =
        Assert.Throws<ArgumentNullException>(fun () -> GithubFile(context, null, 0L, "", "", false) |> ignore)

    [<Fact>]
    let ``Ctor fails on null physical path``() =
        Assert.Throws<ArgumentNullException>(fun () -> GithubFile(context, "", 0L, null, "", false) |> ignore)

    [<Fact>]
    let ``Ctor sets physical path``() =
        //arrange
        let physicalPath = "path/to/file"

        //act
        let sut = GithubFile(context, "", 0L, physicalPath, "", false)

        //assert
        sut.PhysicalPath.Should().Be(physicalPath, "ctor must save file path")

    [<Fact>]
    let ``Ctor fails on null name``() =
        Assert.Throws<ArgumentNullException>(fun () -> GithubFile(context, "", 0L, "", null, false) |> ignore)

    [<Fact>]
    let ``Ctor sets name``() =
        //arrange
        let name = "file_name"

        //act
        let sut = GithubFile(context, "", 0L, "", name, false)

        //assert
        sut.Name.Should().Be(name, "ctor must save file name")

    [<Fact>]
    let ``Ctor sets IsDirectory``() =
        //arrange
        //act
        let fileSut = GithubFile(context, "", 0L, "", "", false)
        let dirSut = GithubFile(context, "", 0L, "", "", true)

        //assert
        fileSut.IsDirectory.Should().BeFalse("directory is not file") |> ignore
        dirSut.IsDirectory.Should().BeTrue("directory is directory") |> ignore

    [<Fact>]
    let ``CreateReadStrem puts data from content client into result stream``() =
        //arrange
        let mockData = Array.create 1024 0uy
        Random().NextBytes(mockData)

        let repoContentsClientMock =
            Mock<IRepositoryContentsClient>()
                .Setup(fun x -> <@ x.GetRawContent(any(), any(), any()) @>)
                .Returns(Task.FromResult(mockData))
                .Create()

        let contextMock = EditingContextContentClientMock(repoContentsClientMock, originRepo, originBranch, forkRepo, currentBranch)

        let sut = GithubFile(contextMock, "", 0L, "", "", false)

        //act
        let stream = sut.CreateReadStream()
        let sr = new BinaryReader(stream)
        let readBytes = sr.ReadBytes(int stream.Length)

        //assert
        Assert.Equal<byte>(mockData, readBytes)
        verify <@ repoContentsClientMock.GetRawContent(any(), any(), any()) @> once

    [<Fact>]
    let ``Exists is always true``() =
        let sut = GithubFile(context, "", 0L, "", "", false)
        sut.Exists.Should().BeTrue("it is written so") |> ignore
