@echo off

rmdir NuGetPackage /s /q
mkdir NuGetPackage
mkdir NuGetPackage\Epicycle.Commons-cs.0.1.2.0
mkdir NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib

copy package.nuspec NuGetPackage\Epicycle.Commons-cs.0.1.2.0\Epicycle.Commons-cs.0.1.2.0.nuspec
copy README.md NuGetPackage\Epicycle.Commons-cs.0.1.2.0\README.md
copy LICENSE NuGetPackage\Epicycle.Commons-cs.0.1.2.0\LICENSE

xcopy bin\net35\Release\Epicycle.Commons_cs.dll NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib\net35\
xcopy bin\net35\Release\Epicycle.Commons_cs.pdb NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib\net35\
xcopy bin\net35\Release\Epicycle.Commons_cs.xml NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib\net35\
xcopy bin\net40\Release\Epicycle.Commons_cs.dll NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib\net40\
xcopy bin\net40\Release\Epicycle.Commons_cs.pdb NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib\net40\
xcopy bin\net40\Release\Epicycle.Commons_cs.xml NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib\net40\
xcopy bin\net45\Release\Epicycle.Commons_cs.dll NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib\net45\
xcopy bin\net45\Release\Epicycle.Commons_cs.pdb NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib\net45\
xcopy bin\net45\Release\Epicycle.Commons_cs.xml NuGetPackage\Epicycle.Commons-cs.0.1.2.0\lib\net45\

pause