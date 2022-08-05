 FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
 WORKDIR /app
 FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
 WORKDIR /src
 COPY ["SpareManagement.csproj", ""]
 RUN dotnet restore "./SpareManagement.csproj"
 COPY . .
 WORKDIR "/src/."
 RUN dotnet build "SpareManagement.csproj" -c Release -o /app/build
 FROM build AS publish
 RUN dotnet publish "SpareManagement.csproj" -c Release -o /app/publish
 FROM base AS final
 WORKDIR /app
 COPY --from=publish /app/publish .
 ENTRYPOINT ["dotnet", "SpareManagement.dll"]