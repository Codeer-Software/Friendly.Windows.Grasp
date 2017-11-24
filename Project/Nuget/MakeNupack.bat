rd /s /q "../ReleaseBinary"
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe" "../Codeer.Friendly.Windows.Grasp/Codeer.Friendly.Windows.Grasp.sln" /rebuild Release
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe" "../Codeer.Friendly.Windows.Grasp/Codeer.Friendly.Windows.Grasp.sln" /rebuild Release-Eng
nuget pack friendly.windows.grasp.nuspec