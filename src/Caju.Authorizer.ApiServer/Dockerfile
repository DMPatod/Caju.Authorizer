# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Caju.Authorizer.ApiServer/Caju.Authorizer.ApiServer.csproj", "src/Caju.Authorizer.ApiServer/"]
COPY ["src/Caju.Authorizer.Application/Caju.Authorizer.Application.csproj", "src/Caju.Authorizer.Application/"]
COPY ["src/Caju.Authorizer.Domain/Caju.Authorizer.Domain.csproj", "src/Caju.Authorizer.Domain/"]
COPY ["src/Caju.Authorizer.Infrastructure/Caju.Authorizer.Infrastructure.csproj", "src/Caju.Authorizer.Infrastructure/"]
RUN dotnet restore "./src/Caju.Authorizer.ApiServer/Caju.Authorizer.ApiServer.csproj"
COPY . .
WORKDIR "/src/src/Caju.Authorizer.ApiServer"
RUN dotnet build "./Caju.Authorizer.ApiServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Caju.Authorizer.ApiServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Caju.Authorizer.ApiServer.dll"]