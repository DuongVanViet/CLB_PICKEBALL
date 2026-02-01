# Deploying the Backend to Render

This document explains how to deploy the .NET backend to Render and how to run it locally for testing.

1) Render setup
- Connect your Git repository to Render and create a new Web Service.
- Render will read `render.yaml` in the repository root. The service is configured to use the Dockerfile at `CLB_PICKEBALL-main/Backend/Dockerfile`.
- In the Render dashboard, configure environment variables / secrets (do NOT commit secrets to the repo). At minimum set:
  - `ConnectionStrings__DefaultConnection` = your Postgres connection string (example below)
  - `ASPNETCORE_ENVIRONMENT` = `Production`
  - Any other app secrets referenced by `appsettings.json` or `Program.cs` (JWT secrets, API keys, etc.)

2) Database / Migrations
- This project contains EF Core migrations in the `Migrations/` folder. Apply them to your production database before or after first deploy.

Option A — from your local machine (requires `dotnet-ef` and network access to the DB):

```powershell
dotnet ef database update --project CLB_PICKEBALL-main/Backend --startup-project CLB_PICKEBALL-main/Backend
```

Option B — run migrations from inside the container (for one-off execution):

```powershell
docker build -t pcm-backend-migrate -f CLB_PICKEBALL-main/Backend/Dockerfile .
docker run --rm -e ConnectionStrings__DefaultConnection="<your-conn>" pcm-backend-migrate dotnet ef database update --no-build
```

3) Local Docker build & run (quick test)

```powershell
# Build image
docker build -t pcm-backend -f CLB_PICKEBALL-main/Backend/Dockerfile .

# Run (set PORT and provide connection string via env)
docker run -e PORT=5000 -e ConnectionStrings__DefaultConnection="Host=...;Username=...;Password=...;Database=..." -p 5000:5000 pcm-backend
```

The app will listen on the mapped port (5000) — visit `http://localhost:5000`.

4) Build & run without Docker

```powershell
cd CLB_PICKEBALL-main/Backend
dotnet restore
dotnet run --urls "http://localhost:5000"
```

5) Recommended Render environment variables (examples)
- `ConnectionStrings__DefaultConnection`: `Host=myhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword`
- `ASPNETCORE_ENVIRONMENT`: `Production`
- `PORT`: Render sets this automatically; the Dockerfile uses `PORT` if present.

6) Notes & troubleshooting
- Keep secrets in Render's dashboard (Environment → Environment Variables / Secrets).
- If your app fails to connect to Postgres, verify network rules and credentials. Consider using Render's managed Postgres addon and copy the connection string to the service's env vars.
- Logs are available from the Render dashboard; use them to troubleshoot startup errors.

If you want, I can also add a small script to run migrations automatically on startup, but that's an optional change.
