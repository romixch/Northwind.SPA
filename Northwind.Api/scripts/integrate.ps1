$solutionFile = "..\Northwind.Api.sln"

& .\clean.ps1
& ..\.nuget\NuGet.exe restore $solutionFile
msbuild $solutionFile /target:Rebuild /v:minimal /p:Configuration=Release