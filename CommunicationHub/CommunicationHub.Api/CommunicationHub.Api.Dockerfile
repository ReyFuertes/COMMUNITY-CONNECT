#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
USER root
RUN apt-get update && apt-get install -y curl
USER app
WORKDIR /app
EXPOSE 8082
HEALTHCHECK --interval=30s --timeout=10s --retries=5 --start-period=30s CMD curl -f http://localhost:8082/health || exit 1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CommunicationHub.Api/CommunicationHub.Api.csproj", "CommunicationHub.Api/"]
COPY ["CommunicationHub.Application/CommunicationHub.Application.csproj", "CommunicationHub.Application/"]
COPY ["CommunicationHub.Domain/CommunicationHub.Domain.csproj", "CommunicationHub.Domain/"]
COPY ["CommunicationHub.Infrastructure/CommunicationHub.Infrastructure.csproj", "CommunicationHub.Infrastructure/"]
RUN dotnet restore "CommunicationHub.Api/CommunicationHub.Api.csproj"
COPY . .
WORKDIR "/src/CommunicationHub.Api"
RUN dotnet build "CommunicationHub.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CommunicationHub.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommunicationHub.Api.dll"]
