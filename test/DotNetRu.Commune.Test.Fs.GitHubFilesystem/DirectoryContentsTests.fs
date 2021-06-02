namespace DotNetRu.Commune.Test.Fs.GitHubFilesystem

open System
open System.Collections
open DotNetRu.Commune.GitHubFilesystem
open Microsoft.Extensions.FileProviders
open Xunit
open FluentAssertions
open Moq

type DirectoryContentsTests() =

    [<Fact>]
    let ``Exists is true``() =
        let sut = new DirectoryContents(Seq.empty<IFileInfo>)
        sut.Exists.Should().BeTrue("it is written so")

    [<Fact>]
    let ``Ctor fails on null input``() =
        Assert.Throws<ArgumentNullException>(fun () -> DirectoryContents(null) |> ignore)

    [<Fact>]
    let ``Ctot stores input and enumerates it``() =
        //arrange
        let input = Array.init 10 (fun _ -> Mock.Of<IFileInfo>())

        //act
        let sut = DirectoryContents(input)

        //assert
        sut.Should().Contain(input, "all origin files must be enumerated")

    [<Fact>]
    let ``DirectoryContents can be accessed as non-generic IEnumerable``() =
        //arrange
        let input = Array.init 10 (fun _ -> Mock.Of<IFileInfo>())
        let sut = DirectoryContents(input)
        let outputArr = Array.zeroCreate<obj> 10

        //act
        let casted = sut :> IEnumerable
        let enumerator = casted.GetEnumerator()
        let mutable i = 0
        while enumerator.MoveNext() do
            outputArr.[i] <- enumerator.Current
            i <- i + 1

        //assert
        outputArr.Should().Contain(input, "all origin files must be enumerated")
