FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
USER root
RUN apt-get update && apt-get install -y curl
USER app
WORKDIR /app
EXPOSE 8086

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BookingService.Api/BookingService.Api.csproj", "BookingService.Api/"]
COPY ["BookingService.Application/BookingService.Application.csproj", "BookingService.Application/"]
COPY ["BookingService.Domain/BookingService.Domain.csproj", "BookingService.Domain/"]
COPY ["BookingService.Infrastructure/BookingService.Infrastructure.csproj", "BookingService.Infrastructure/"]
RUN dotnet restore "BookingService.Api/BookingService.Api.csproj"
COPY . .
WORKDIR "/src/BookingService.Api"
RUN dotnet build "BookingService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BookingService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingService.Api.dll"]