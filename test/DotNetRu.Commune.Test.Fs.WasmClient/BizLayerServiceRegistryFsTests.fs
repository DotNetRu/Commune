namespace DotNetRu.Commune.Test.Fs.WasmClient

open System
open DotNetRu.Commune.WasmClient
open Xunit

type BizLayerServiceRegistryFsTests() =
    [<Fact>]
    let ``AddBizLogic when ServiceCollection is null Throws ArgumentNullException`` () =
        Assert.Throws<ArgumentNullException>(fun() -> BizLayerServiceRegistry.AddBizLogic(null) |> ignore)
