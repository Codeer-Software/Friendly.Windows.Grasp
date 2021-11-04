rd /s /q "../ReleaseBinary"
"%DevEnvDir%devenv.exe" "../Codeer.Friendly.Windows.Grasp/Codeer.Friendly.Windows.Grasp.sln" /rebuild Release
"%DevEnvDir%devenv.exe" "../Codeer.Friendly.Windows.Grasp/Codeer.Friendly.Windows.Grasp.sln" /rebuild Release-Eng
nuget pack friendly.windows.grasp.nuspec