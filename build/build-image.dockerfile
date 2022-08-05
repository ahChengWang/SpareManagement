### Build Stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dotnet-build-env
COPY ./source /src
WORKDIR /src
RUN dotnet publish SpareManagement.csproj -o /publish --configuration Release

### Publish Stage
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=dotnet-build-env /publish .
ENTRYPOINT ["dotnet", "SpareManagement.dll"]