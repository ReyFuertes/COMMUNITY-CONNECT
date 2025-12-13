FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
USER root
RUN apt-get update && apt-get install -y curl
USER app
WORKDIR /app
EXPOSE 8087
HEALTHCHECK --interval=30s --timeout=10s --retries=5 --start-period=30s CMD curl -f http://localhost:8087/health || exit 1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DocumentService.Api/DocumentService.Api.csproj", "DocumentService.Api/"]
COPY ["DocumentService.Application/DocumentService.Application.csproj", "DocumentService.Application/"]
COPY ["DocumentService.Domain/DocumentService.Domain.csproj", "DocumentService.Domain/"]
COPY ["DocumentService.Infrastructure/DocumentService.Infrastructure.csproj", "DocumentService.Infrastructure/"]
RUN dotnet restore "DocumentService.Api/DocumentService.Api.csproj"
COPY . .
WORKDIR "/src/DocumentService.Api"
RUN dotnet build "DocumentService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DocumentService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentService.Api.dll"]