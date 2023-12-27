ARG BUILD_CONFIGURATION=Release

#############
### Build ###
#############
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION

COPY . .
RUN dotnet restore "./src/KitchenHell.ActivityGenerators"
RUN dotnet build "./src/KitchenHell.ActivityGenerators" -c $BUILD_CONFIGURATION --no-restore

###############
### Publish ###
###############
FROM build AS publish
ARG BUILD_CONFIGURATION
RUN dotnet publish "./src/KitchenHell.ActivityGenerators" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore --no-build

#############
### Final ###
#############
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl \
    && rm -rf /var/lib/apt/lists/*

EXPOSE 8080
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KitchenHell.ActivityGenerators.dll"]
