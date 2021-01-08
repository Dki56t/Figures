# Build solution
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["*.sln", "./"]
COPY ["API/*.csproj", "API/"]
COPY ["Core/*.csproj", "Core/"]
COPY ["Implementation.DataAccess/*.csproj", "Implementation.DataAccess/"]
COPY ["Infrastructure/*.csproj", "Infrastructure/"]
COPY ["Tests/*.csproj", "Tests/"]
RUN dotnet restore
COPY . .

WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

# Publish API
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll"]