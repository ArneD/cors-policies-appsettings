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
                Framework = "netcoreapp1.1";                
                Output = "../../" @@ tempBuildDir;
                Configuration = "Release" })    
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