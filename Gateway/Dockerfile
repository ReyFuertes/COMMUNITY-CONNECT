FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src/CommunityConnect.Gateway
COPY CommunityConnect.Gateway.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CommunityConnect.Gateway.dll"]