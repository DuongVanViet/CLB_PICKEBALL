FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore (Backend folder structure)
COPY Backend/*.csproj ./Backend/
RUN dotnet restore "Backend/PcmBackend.csproj"

# Copy everything else and publish
COPY . .
WORKDIR /src/Backend
RUN dotnet publish PcmBackend.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Render uses PORT env var; fallback to 8080 if not set
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "PcmBackend.dll"]
