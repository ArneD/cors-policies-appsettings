#r "../packages/FAKE/tools/FakeLib.dll"
open Fake

let tempBuildDir = @"./build/temp"
let distDir = @"./src/web/dist"


Target "RestorePackages" (fun _ ->
    DotNetCli.Restore (fun p -> 
            { p with 
                 WorkingDir = "./src/CorsPolicySettings";
                 NoCache = true })
)

Target "Build" (fun _ ->
    let properties = [                    
                    "Optimize", "True"
                    "DebugSymbols", "False"
                    "DebugType", "None"
                    "ExcludeGeneratedDebugSymbol", "True"
                    "AllowedReferenceRelatedFileExtensions", "None"]

    !! "./src/CorsPolicySettings/CorsPolicySettings.csproj"
        |> MSBuildReleaseExt tempBuildDir properties "Build"
        |> Log "Build-Output: "
)

Target "Copy" (fun _ ->
    let apiDir = tempBuildDir @@ "CorsPolicySettings.dll"
    XCopy apiDir @"./artifacts/"
)

Target "Clean" (fun _ ->
    CleanDirs ["artifacts"; tempBuildDir]
)

Target "All" DoNothing

"Clean"
    ==> "RestorePackages"
    ==> "Build"
    ==> "Copy"
    ==> "All"

Run "All"