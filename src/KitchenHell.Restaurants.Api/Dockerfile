FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /repoRoot

RUN dotnet tool install --global dotnet-ef 

COPY . .
RUN dotnet restore "./src/KitchenHell.Restaurants.Api"
RUN dotnet build "./src/KitchenHell.Restaurants.Api" -c $BUILD_CONFIGURATION --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./src/KitchenHell.Restaurants.Api" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore --no-build
RUN dotnet ef migrations bundle -p "./src/KitchenHell.Restaurants.Persistence/"  \
    -r linux-x64  \
    --configuration $BUILD_CONFIGURATION  \
    -o /app/publish/efbundle  \
    --self-contained  \
    --no-build  \
    --verbose

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
EXPOSE 8080
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KitchenHell.Restaurants.Api.dll"]
