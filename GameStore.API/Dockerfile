# =========================
# Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["GameStore.Api/GameStore.Api.csproj", "GameStore.Api/"]
COPY ["GameStore.Application/GameStore.Application.csproj", "GameStore.Application/"]
COPY ["GameStore.Domain/GameStore.Domain.csproj", "GameStore.Domain/"]
COPY ["GameStore.Infrastructure/GameStore.Infrastructure.csproj", "GameStore.Infrastructure/"]

RUN dotnet restore "GameStore.Api/GameStore.Api.csproj"

COPY . .

WORKDIR "/src/GameStore.Api"

RUN dotnet publish "GameStore.Api.csproj" \
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

ENTRYPOINT ["dotnet", "GameStore.Api.dll"]