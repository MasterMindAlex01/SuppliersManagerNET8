# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Applications/SuppliersManager.Api/SuppliersManager.Api.csproj", "src/Applications/SuppliersManager.Api/"]
COPY ["src/Infrastructure/SuppliersManager.Infrastructure/SuppliersManager.Infrastructure.MongoDBDriver.csproj", "src/Infrastructure/SuppliersManager.Infrastructure/"]
COPY ["src/Core/SuppliersManager.Application/SuppliersManager.Application.csproj", "src/Core/SuppliersManager.Application/"]
COPY ["src/Core/SuppliersManager.Domain/SuppliersManager.Domain.csproj", "src/Core/SuppliersManager.Domain/"]
COPY ["src/Core/SuppliersManager.Shared/SuppliersManager.Shared.csproj", "src/Core/SuppliersManager.Shared/"]
RUN dotnet restore "./src/Applications/SuppliersManager.Api/SuppliersManager.Api.csproj"
COPY . .
WORKDIR "/src/src/Applications/SuppliersManager.Api"
RUN dotnet build "./SuppliersManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SuppliersManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuppliersManager.Api.dll"]