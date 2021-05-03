namespace MyMocks

open Octokit

type UserMock() =
    inherit User()
    member this.PubLogin with set value = this.Login <- value

type RepositoryMock() =
    inherit Repository()
    member this.PubName with set value = this.Name <- value
    member this.PubOwner with set value = this.Owner <- value
    member this.PubId with set value = this.Id <- value
