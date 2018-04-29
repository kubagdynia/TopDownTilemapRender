dotnet publish -r win10-x64 -c release

rem removing the console window
.\Tools\editbin.exe /subsystem:windows .\TopDownTilemapRender\bin\Release\netcoreapp2.0\win10-x64\TopDownTilemapRender.exe
.\Tools\editbin.exe /subsystem:windows .\TopDownTilemapRender\bin\Release\netcoreapp2.0\win10-x64\publish\TopDownTilemapRender.exe