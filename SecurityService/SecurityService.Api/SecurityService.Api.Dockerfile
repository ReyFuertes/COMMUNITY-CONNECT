FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
USER root
RUN apt-get update && apt-get install -y curl
USER app
WORKDIR /app
EXPOSE 8085
HEALTHCHECK --interval=30s --timeout=10s --retries=5 --start-period=30s CMD curl -f http://localhost:8085/health || exit 1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SecurityService.Api/SecurityService.Api.csproj", "SecurityService.Api/"]
COPY ["SecurityService.Application/SecurityService.Application.csproj", "SecurityService.Application/"]
COPY ["SecurityService.Domain/SecurityService.Domain.csproj", "SecurityService.Domain/"]
COPY ["SecurityService.Infrastructure/SecurityService.Infrastructure.csproj", "SecurityService.Infrastructure/"]
RUN dotnet restore "SecurityService.Api/SecurityService.Api.csproj"
COPY . .
WORKDIR "/src/SecurityService.Api"
RUN dotnet build "SecurityService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SecurityService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecurityService.Api.dll"]