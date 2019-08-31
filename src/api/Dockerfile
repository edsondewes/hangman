FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Hangman.Api/Hangman.Api.csproj", "Hangman.Api/"]
COPY ["Hangman.Core/Hangman.Core.csproj", "Hangman.Core/"]
COPY ["Hangman.Persistence.InMemory/Hangman.Persistence.InMemory.csproj", "Hangman.Persistence.InMemory/"]
COPY ["Hangman.Persistence.MongoDb/Hangman.Persistence.MongoDb.csproj", "Hangman.Persistence.MongoDb/"]
RUN dotnet restore "Hangman.Api/Hangman.Api.csproj"
COPY . .
WORKDIR "/src/Hangman.Api"
RUN dotnet publish "Hangman.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Hangman.Api.dll"]