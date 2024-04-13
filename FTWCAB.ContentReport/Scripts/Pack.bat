@echo off

call Scripts\BuildFrontend.bat

powershell -command "Compress-Archive -Path .\ClientResources\, .\FTWCAB.ContentReport.Views\, module.config -DestinationPath .\FTWCAB.ContentReport.zip -CompressionLevel Optimal -Force"

dotnet pack -o .

dotnet nuget push --skip-duplicate --source "testfeed" --api-key az *.nupkg --interactive

del FTWCAB.ContentReport.zip
del *.nupkg