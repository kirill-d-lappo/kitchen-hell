ARG BUILD_CONFIGURATION=Release

#############
### Build ###
#############
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION

ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /repoRoot

RUN dotnet tool install --global dotnet-ef 

COPY . .
RUN dotnet restore "./src/KitchenHell.Api"
RUN dotnet build "./src/KitchenHell.Api" -c $BUILD_CONFIGURATION --no-restore

###############
### Publish ###
###############
FROM build AS publish
ARG BUILD_CONFIGURATION
RUN dotnet publish "./src/KitchenHell.Api" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore --no-build
RUN dotnet ef migrations bundle -p "./src/KitchenHell.Orders.Persistence/"  \
    -r linux-x64  \
    --configuration $BUILD_CONFIGURATION  \
    -o /app/publish/efbundle-orders  \
    --self-contained  \
    --no-build  \
    --verbose

RUN dotnet ef migrations bundle -p "./src/KitchenHell.Restaurants.Persistence/"  \
    -r linux-x64  \
    --configuration $BUILD_CONFIGURATION  \
    -o /app/publish/efbundle-restaurants  \
    --self-contained  \
    --no-build  \
    --verbose

#############
### Final ###
#############
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
EXPOSE 8080
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KitchenHell.Api.dll"]
