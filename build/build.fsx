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

    DotNetCli.Build (fun p -> 
            { p with
                Project = "./src/CorsPolicySettings/CorsPolicySettings.csproj";
                Framework = "netstandard1.6";                
                Output = "../../" @@ tempBuildDir;
                Configuration = "Release";
                AdditionalArgs = ["/p:VersionPrefix=1.0.3"] })    
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