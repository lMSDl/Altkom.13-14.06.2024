dotnet test --collect:"XPlat Code Coverage"

dotnet tool install -g dotnet-reportgenerator-globaltool

reportgenerator -reports:"Path/do/raportu.xml" -targetdir:"folder/docelowy"
