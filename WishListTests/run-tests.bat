cmd /c "dotnet add package coverlet.msbuild"
cmd /c "dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/coverage.cobertura.xml"
cmd /c "reportgenerator "-reports:.\TestResults\coverage.cobertura.xml" "-targetdir:.\TestResults" -reporttypes:Html"
start ./TestResults/index.htm