:: Reference project from "Epi dev"-project, add the below in order to get the zip copied to modules\_protected folder
:: <Target Name="PreBuild" BeforeTargets="PreBuildEvent">
:: 	<Exec Command="call [PATH TO THIS FOLDER]\DevPack.bat" />
:: </Target>
:: Also add a project reference and add the services.AddFtwContentReports(); to ConfigureServices()

@echo off

set oldpath=%CD%
SET mypath=%~dp0
cd %mypath:~0,-1%\..

call Scripts\BuildFrontend.bat

if not exist %oldpath%\modules\_protected\FTWCAB.ContentReport mkdir %oldpath%\modules\_protected\FTWCAB.ContentReport
powershell -command "Compress-Archive -Path .\ClientResources\, .\FTWCAB.ContentReport.Views\, module.config -DestinationPath %oldpath%\modules\_protected\FTWCAB.ContentReport\FTWCAB.ContentReport.zip -CompressionLevel Optimal -Force"
echo Copied FTWCAB.ContentReport.zip to %oldpath%\modules\_protected\FTWCAB.ContentReport\FTWCAB.ContentReport.zip

cd %oldpath%