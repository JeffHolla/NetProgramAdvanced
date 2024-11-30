@echo off

echo [Info] : Current directory - %cd%
echo [Info] : Solution directory - %1
echo.

echo [Info] : Changing current directory to solution directory
cd %1
echo [Info] : Current directory is %cd%
echo.

echo [Info] : Starting the sonarscanner...
echo.
dotnet sonarscanner begin /k:"CartServiceKey" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_0f08a72491520929b82c13ff3c59b66178d83a64"

echo.
echo [Info] : Starting project build...
echo.
dotnet build

echo.
echo [Info] : Finishing the sonarscanner...
echo.
dotnet sonarscanner end /d:sonar.token="sqp_0f08a72491520929b82c13ff3c59b66178d83a64"
