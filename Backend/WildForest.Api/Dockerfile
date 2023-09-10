FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Backend/WildForest.Api/WildForest.Api.csproj", "Backend/WildForest.Api/"]
RUN dotnet restore "Backend/WildForest.Api/WildForest.Api.csproj"
COPY . .
WORKDIR "/src/Backend/WildForest.Api"
RUN dotnet build "WildForest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WildForest.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WildForest.Api.dll"]