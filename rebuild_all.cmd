@echo off

cd projects
msbuild Epicycle.Commons.net35.sln /t:Clean,Build /p:Configuration=Debug
msbuild Epicycle.Commons.net35.sln /t:Clean,Build /p:Configuration=Release
msbuild Epicycle.Commons.net40.sln /t:Clean,Build /p:Configuration=Debug
msbuild Epicycle.Commons.net40.sln /t:Clean,Build /p:Configuration=Release
msbuild Epicycle.Commons.net45.sln /t:Clean,Build /p:Configuration=Debug
msbuild Epicycle.Commons.net45.sln /t:Clean,Build /p:Configuration=Release

pause
