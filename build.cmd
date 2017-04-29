call ".nuget/Nuget.exe" install FAKE -OutputDirectory "packages" -ExcludeVersion
call "packages/FAKE/tools/Fake.exe" "build/build.fsx"