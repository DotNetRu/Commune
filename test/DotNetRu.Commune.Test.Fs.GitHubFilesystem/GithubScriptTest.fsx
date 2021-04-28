#r "nuget: Octokit"
open Octokit
open Octokit.Internal

let ghclient = new GitHubClient(new ProductHeaderValue("FSXCLIENT"), new InMemoryCredentialStore(new Credentials("", AuthenticationType.Bearer)))

let auditContents = ghclient.Repository.Content.GetAllContents("DotNetRu", "Audit")
                    |> Async.AwaitTask
                    |> Async.RunSynchronously
let rec contentMapper (content : RepositoryContentInfo) =
    match content.Type.Value with
    | ContentType.File -> seq {content.Path}
    | ContentType.Dir -> ghclient.Repository.Content.GetAllContents("DotNetRu", "Audit", content.Path) |> Async.AwaitTask |> Async.RunSynchronously |> Seq.collect contentMapper
    | _ -> Seq.empty

let flattenedCOntents = auditContents |> Seq.map contentMapper |> Seq.collect (fun x -> x)

flattenedCOntents |> Seq.iter (printfn "%s")
