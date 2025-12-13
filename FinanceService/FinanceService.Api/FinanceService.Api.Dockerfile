FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
USER root
RUN apt-get update && apt-get install -y curl
USER app
WORKDIR /app
EXPOSE 8084
HEALTHCHECK --interval=30s --timeout=10s --retries=5 --start-period=30s CMD curl -f http://localhost:8084/health || exit 1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FinanceService.Api/FinanceService.Api.csproj", "FinanceService.Api/"]
COPY ["FinanceService.Application/FinanceService.Application.csproj", "FinanceService.Application/"]
COPY ["FinanceService.Domain/FinanceService.Domain.csproj", "FinanceService.Domain/"]
COPY ["FinanceService.Infrastructure/FinanceService.Infrastructure.csproj", "FinanceService.Infrastructure/"]
RUN dotnet restore "FinanceService.Api/FinanceService.Api.csproj"
COPY . .
WORKDIR "/src/FinanceService.Api"
RUN dotnet build "FinanceService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinanceService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceService.Api.dll"]