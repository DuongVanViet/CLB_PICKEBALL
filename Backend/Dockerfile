ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY Backend/*.csproj ./Backend/
RUN dotnet restore "Backend/PcmBackend.csproj"

# Copy everything else and publish
COPY . .
WORKDIR /src/Backend
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Let Render (or caller) set the PORT env var; default to 5000 for local testing
ENV ASPNETCORE_URLS=http://+:${PORT:-5000}
EXPOSE 5000

ENTRYPOINT ["dotnet", "PcmBackend.dll"]
