@echo off
@rem ***************************************
@rem Arguments
@rem %1: Build Configuration (Release/Debug)
@rem ***************************************

if "%1"=="" ( 
@echo Error: Speficy build configuration [Release/Debug]
@exit /B
)

mkdir bin\%1\netcoreapp2.0

if "%1"=="Release" (
  copy ..\..\..\openpose\build\x64\%1\openpose.dll bin\%1\netcoreapp2.0\openpose.dll /y
) else if "%1"=="Debug" (
  copy ..\..\..\openpose\build\x64\%1\openposed.dll bin\%1\netcoreapp2.0\openposed.dll /y
  copy ..\..\..\openpose\build\x64\%1\openposed.pdb bin\%1\netcoreapp2.0\openposed.pdb /y
)

xcopy ..\..\..\openpose\build\bin\*  bin\%1\netcoreapp2.0 /q /y

if "%1"=="Release" (
  copy ..\..\..\src\OpenPoseDotNet.Native\build\%1\OpenPoseDotNet.Native.dll bin\%1\netcoreapp2.0 /y
) else if "%1"=="Debug" (
  copy ..\..\..\src\OpenPoseDotNet.Native\build\%1\OpenPoseDotNet.Native.dll bin\%1\netcoreapp2.0 /y
  copy ..\..\..\src\OpenPoseDotNet.Native\build\%1\OpenPoseDotNet.Native.pdb bin\%1\netcoreapp2.0 /y
)