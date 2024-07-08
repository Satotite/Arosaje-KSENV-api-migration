FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7239

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Arosaje-KSENV/Arosaje-KSENV.csproj", "Arosaje-KSENV/"]
RUN dotnet restore "./Arosaje-KSENV/Arosaje-KSENV.csproj"
COPY . .
WORKDIR "/src/Arosaje-KSENV"
RUN dotnet build "./Arosaje-KSENV.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Arosaje-KSENV.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .docker
ENTRYPOINT ["dotnet", "Arosaje-KSENV.dll"]
