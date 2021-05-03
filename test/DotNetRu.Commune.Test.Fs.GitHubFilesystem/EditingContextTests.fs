namespace DotNetRu.Commune.Test.Fs.GitHubFilesystem

open DotNetRu.Commune.GitHubFilesystem
open System.Threading.Tasks
open Foq
open Octokit
open Xunit

type EditingContextPrClientMock =
    inherit EditingContext
    val prClient : IPullRequestsClient

    new(pullRequestsClient : IPullRequestsClient, originRepo : Repository, originBranch : Reference, forkRepo : Repository, currentBranch : Reference) =
        {
            inherit EditingContext(Mock.Of<IGitHubClient>(), originRepo, originBranch, forkRepo, currentBranch)
            prClient = pullRequestsClient
        }

    override this.PullRequestsClient
        with get() = this.prClient


type EditingContextTests() =
    class
        [<Fact>]
        let ``Commit when called calls PullRequestClient``() =
            //arrange
            let prClientMock = Mock<IPullRequestsClient>().Setup(fun x -> <@ x.Create(any(), any()) @>).Returns(Task.FromResult(PullRequest())).Create()
            let originRepo = Repository(42L)
            let originBranch = Reference("origin-master", "", "", TagObject())
            let currentBranch = Reference("current-branch", "", "", TagObject())

            let sut = EditingContextPrClientMock(prClientMock, originRepo, originBranch, Repository(), currentBranch)

            //act
            sut.Commit() |> Async.AwaitTask |> Async.RunSynchronously

            //assert
            verify <@ prClientMock.Create(is (fun originId -> originId = 42L), is (fun (request : NewPullRequest) -> request.Draft.HasValue && request.Draft.Value && request.Head = "current-branch" && request.Base = "origin-master")) @> once
    end

