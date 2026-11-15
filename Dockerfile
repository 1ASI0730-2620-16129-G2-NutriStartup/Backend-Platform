FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY BackendPlatform.API/BackendPlatform.API.csproj BackendPlatform.API/
RUN dotnet restore BackendPlatform.API/BackendPlatform.API.csproj

COPY . .
WORKDIR /src/BackendPlatform.API
RUN dotnet publish BackendPlatform.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 10000
ENTRYPOINT ["sh", "-c", "dotnet BackendPlatform.API.dll --urls http://0.0.0.0:${PORT:-10000}"]
