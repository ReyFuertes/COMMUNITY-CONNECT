FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
USER root
RUN apt-get update && apt-get install -y curl
USER app
WORKDIR /app
EXPOSE 8088
HEALTHCHECK --interval=30s --timeout=10s --retries=5 --start-period=30s CMD curl -f http://localhost:8088/health || exit 1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EngagementService.Api/EngagementService.Api.csproj", "EngagementService.Api/"]
COPY ["EngagementService.Application/EngagementService.Application.csproj", "EngagementService.Application/"]
COPY ["EngagementService.Domain/EngagementService.Domain.csproj", "EngagementService.Domain/"]
COPY ["EngagementService.Infrastructure/EngagementService.Infrastructure.csproj", "EngagementService.Infrastructure/"]
RUN dotnet restore "EngagementService.Api/EngagementService.Api.csproj"
COPY . .
WORKDIR "/src/EngagementService.Api"
RUN dotnet build "EngagementService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EngagementService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EngagementService.Api.dll"]