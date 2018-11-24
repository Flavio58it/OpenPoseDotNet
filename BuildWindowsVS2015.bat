@echo off
@rem ***************************************
@rem Arguments
@rem %1: Build Configuration (Release/Debug)
@rem ***************************************

if "%1"=="" ( 
@echo Error: Speficy build configuration [Release/Debug]
@exit /B
)

set CURDIR=%cd%
set OUTPUT=build
cd openpose
mkdir %OUTPUT%
cd %OUTPUT%
cmake -G "Visual Studio 14 2015 Win64" ^
      ..
cmake --build . --config %1
cd %CURDIR%