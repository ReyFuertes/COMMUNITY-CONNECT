#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
USER root
RUN apt-get update && apt-get install -y curl
USER app
WORKDIR /app
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MaintenanceService.Api/MaintenanceService.Api.csproj", "MaintenanceService.Api/"]
COPY ["MaintenanceService.Application/MaintenanceService.Application.csproj", "MaintenanceService.Application/"]
COPY ["MaintenanceService.Domain/MaintenanceService.Domain.csproj", "MaintenanceService.Domain/"]
COPY ["MaintenanceService.Infrastructure/MaintenanceService.Infrastructure.csproj", "MaintenanceService.Infrastructure/"]
RUN dotnet restore "MaintenanceService.Api/MaintenanceService.Api.csproj"
COPY . .
WORKDIR "/src/MaintenanceService.Api"
RUN dotnet build "MaintenanceService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MaintenanceService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MaintenanceService.Api.dll"]
