#r "nuget: Octokit"
#r "nuget: Microsoft.Extensions.FileProviders.Abstractions"
#r "../../DotNetRu.Commune.GitHubFilesystem/bin/Debug/net5.0/DotNetRu.Commune.GitHubFilesystem.dll"

open DotNetRu.Commune.GitHubFilesystem
open System
open System.IO

let client = new GitHubFilesystem()
client.StartContext("", "DEMO", "SelectFromGroup-By") |> Async.AwaitTask |> Async.RunSynchronously

let rootContents = client.GetDirectoryContents("/")
let readme = client.GetFileInfo("README.md")
let readmeStream = readme.CreateReadStream()
let sw = new StreamWriter(readmeStream)
sw.Write("test data new at {0}", DateTime.Now.ToString())
sw.Flush()
client.CommitChanges() |> Async.AwaitTask |> Async.RunSynchronously
