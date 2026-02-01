FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Copy Backend folder contents into the build stage
# (when dockerfilePath: Backend/.Dockerfile, build context is Backend folder)
WORKDIR /src
COPY . /src

# Publish the Backend project
RUN dotnet publish PcmBackend.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "PcmBackend.dll"]
