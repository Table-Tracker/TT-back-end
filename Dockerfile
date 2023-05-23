# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY TableTracker/*.csproj ./TableTracker/
COPY TableTracker.Application/*.csproj ./TableTracker.Application/
COPY TableTracker.Domain/*.csproj ./TableTracker.Domain/
COPY TableTracker.Infrastructure/*.csproj ./TableTracker.Infrastructure/
COPY TableTracker.Tests/*.csproj ./TableTracker.Tests/
RUN dotnet restore

# copy everything else and build app
COPY TableTracker/. ./TableTracker/
COPY TableTracker.Application/. ./TableTracker.Application/
COPY TableTracker.Domain/. ./TableTracker.Domain/
COPY TableTracker.Infrastructure/. ./TableTracker.Infrastructure/
COPY TableTracker.Tests/. ./TableTracker.Tests/
WORKDIR /source
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "TableTracker.dll"]