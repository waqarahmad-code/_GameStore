# =========================
# Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["GameStore.Client/GameStore.Client.csproj", "GameStore.Client/"]

RUN dotnet restore "GameStore.Client/GameStore.Client.csproj"

COPY . .

WORKDIR "/src/GameStore.Client"

RUN dotnet publish "GameStore.Client.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


# =========================
# Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_HTTP_PORTS=8080

ENTRYPOINT ["dotnet", "GameStore.Client.dll"]