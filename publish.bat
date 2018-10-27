@echo off

call .paket\paket restore
call packages\FSharp.Formatting.CommandTool\tools\fsformatting.exe literate --processDirectory --lineNumbers true --inputDirectory  "code" --outputDirectory "_posts"

call dotnet build .\filefixer\filefixer.fsproj
call dotnet run --project .\filefixer\filefixer.fsproj _posts

git add --all .
git commit -a -m %1
git push