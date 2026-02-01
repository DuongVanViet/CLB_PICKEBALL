ARG DOTNET_VERSION=8.0

# ===== BUILD STAGE =====
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy csproj and restore
COPY Backend/*.csproj Backend/
RUN dotnet restore "Backend/PcmBackend.csproj"

# Copy source and publish
COPY . .
WORKDIR /src/Backend
RUN dotnet publish -c Release -o /app/publish --no-restore

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Render uses port 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "PcmBackend.dll"]
