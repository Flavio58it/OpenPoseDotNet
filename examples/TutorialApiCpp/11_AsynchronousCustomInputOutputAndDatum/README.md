# Asynchronous Custom Input Output And Datum

## Quick Start

#### 1. Build OpenPose

````dos
> cd <OpenPoseDotNet_dir>
> BuildWindowsVS2015.bat <Debug/Release>
````

#### 2. Build OpenPoseDotNet.Native

````dos
> cd <OpenPoseDotNet_dir>\src\OpenPoseDotNet.Native
> BuildWindowsVS2015.bat <Debug/Release>
````

#### 3. Try Tutorial

````dos
> cd <OpenPoseDotNet_dir>\examples\TutorialApiCpp\11_AsynchronousCustomInputOutputAndDatum
> CopyOpenPose.bat <Debug/Release>
> dotnet run -c Release -i "<OpenPoseDotNet_dir>\openpose\examples\media"
````

<img src="images/example_turorial_8.gif"/>