#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
USER root
RUN apt-get update && apt-get install -y curl
USER app
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UserAndUnitManagement.Api/CommunityConnect.UserAndUnitMgmtAPI.csproj", "UserAndUnitManagement.Api/"]
COPY ["UserAndUnitManagement.Application/UserAndUnitManagement.Application.csproj", "UserAndUnitManagement.Application/"]
COPY ["UserAndUnitManagement.Domain/UserAndUnitManagement.Domain.csproj", "UserAndUnitManagement.Domain/"]
COPY ["UserAndUnitManagement.Infrastructure/UserAndUnitManagement.Infrastructure.csproj", "UserAndUnitManagement.Infrastructure/"]
RUN dotnet restore "UserAndUnitManagement.Api/CommunityConnect.UserAndUnitMgmtAPI.csproj"
COPY . .
WORKDIR "/src/UserAndUnitManagement.Api"
RUN dotnet build "CommunityConnect.UserAndUnitMgmtAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CommunityConnect.UserAndUnitMgmtAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommunityConnect.UserAndUnitMgmtAPI.dll"]
