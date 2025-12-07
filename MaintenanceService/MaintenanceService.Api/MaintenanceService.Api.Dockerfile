#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MaintenanceService/MaintenanceService.Api/MaintenanceService.Api.csproj", "MaintenanceService/MaintenanceService.Api/"]
COPY ["MaintenanceService/MaintenanceService.Application/MaintenanceService.Application.csproj", "MaintenanceService/MaintenanceService.Application/"]
COPY ["MaintenanceService/MaintenanceService.Domain/MaintenanceService.Domain.csproj", "MaintenanceService/MaintenanceService.Domain/"]
COPY ["MaintenanceService/MaintenanceService.Infrastructure/MaintenanceService.Infrastructure.csproj", "MaintenanceService/MaintenanceService.Infrastructure/"]
RUN dotnet restore "MaintenanceService/MaintenanceService.Api/MaintenanceService.Api.csproj"
COPY . .
WORKDIR "/src/MaintenanceService/MaintenanceService.Api"
RUN dotnet build "MaintenanceService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MaintenanceService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MaintenanceService.Api.dll"]
