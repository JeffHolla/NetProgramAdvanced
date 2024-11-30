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
dotnet sonarscanner begin /k:"CatalogServiceKey" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_9a633b9dfd3054822b85165bfb4adbc1369c1fbf"

echo.
echo [Info] : Starting project build...
echo.
dotnet build

echo.
echo [Info] : Finishing the sonarscanner...
echo.
dotnet sonarscanner end /d:sonar.token="sqp_9a633b9dfd3054822b85165bfb4adbc1369c1fbf"
