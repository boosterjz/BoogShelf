FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY *.sln .
COPY BookShelf/*.csproj ./BookShelf/
COPY BookShelf.Tests/*.csproj ./BookShelf.Tests/
RUN dotnet restore

COPY BookShelf/. ./BookShelf/
WORKDIR /src/BookShelf
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "BookShelf.dll"]