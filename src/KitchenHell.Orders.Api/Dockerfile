FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /repoRoot

# restore packages and cache layers in docker context
COPY ["./*.props", "./"]
COPY ["./src/KitchenHell.Orders.Api/KitchenHell.Orders.Api.csproj", "./src/KitchenHell.Orders.Api/"]
COPY ["./src/KitchenHell.Orders.Persistence/KitchenHell.Orders.Persistence.csproj", "./src/KitchenHell.Orders.Persistence/"]
RUN dotnet restore "./src/KitchenHell.Orders.Api"

# not cached, so after restore operations
RUN dotnet tool install --global dotnet-ef 

# copy the rest of the files and build using already restored files
COPY . .
RUN dotnet build "./src/KitchenHell.Orders.Api" -c $BUILD_CONFIGURATION --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./src/KitchenHell.Orders.Api" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore --no-build
RUN dotnet ef migrations bundle -p "./src/KitchenHell.Orders.Persistence/"  \
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
ENTRYPOINT ["dotnet", "KitchenHell.dll"]
