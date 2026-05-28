# mpi-denisf

MVP marketplace integration platform with:
- **Backend**: ASP.NET Core Web API (.NET)
- **Frontend**: Vue 3 + Vite admin panel

## Project structure

- `backend/MarketplaceIntegration.Api` — .NET API with modular domain/application/infrastructure layout
- `frontend` — Vue admin UI (Dashboard, Products, Channels, Sync Jobs)

## Backend architecture summary

Backend is organized for future connector growth:

- **Domain**
  - Entities: `Products`, `ProductVariants`, `Channels`, `ChannelAccounts`, `ChannelListings`, `Inventory`, `SyncJobs`, `SyncErrors`
  - Enums for channel type and sync status
- **Application**
  - DTOs and services for use cases
  - Repository abstractions for core resources
- **Infrastructure**
  - EF Core `MarketplaceDbContext` (PostgreSQL-ready, InMemory default for MVP run)
  - Repository implementations
  - Marketplace connector abstraction `IMarketplaceConnector`
  - Stub connectors for Amazon and eBay
  - Background sync worker skeleton with in-memory queue
- **API**
  - Controllers with JSON responses
  - CRUD endpoints for Products and Channels
  - List endpoints for other MVP resources
  - Sync trigger endpoint: `POST /api/syncjobs/trigger`
  - Swagger/OpenAPI and health check endpoint (`/health`)

## Run backend

Prerequisites: .NET SDK 10

```bash
cd backend/MarketplaceIntegration.Api
dotnet restore
dotnet run
```

Backend default behavior:
- Uses **InMemory** database (`Database:Provider=InMemory`)
- To switch to PostgreSQL, set:
  - `Database__Provider=Postgres`
  - `ConnectionStrings__DefaultConnection=<your connection string>`

Swagger UI is available when running at:
- `http://localhost:5000/swagger` (or current configured port)

## Run frontend

Prerequisites: Node.js 20+

```bash
cd frontend
npm install
npm run dev
```

Optional API URL override:
- `VITE_API_BASE_URL=http://localhost:5000`

## Next steps for real marketplace integrations

1. Replace stub connectors with production API adapters (auth, mapping, retries, rate limits)
2. Add robust queueing (RabbitMQ/SQS) for sync orchestration
3. Add migrations and production PostgreSQL deployment setup
4. Expand validation/mapping rules per marketplace category and attributes
5. Add authentication/authorization and audit logging in admin/API
6. Add automated integration and end-to-end tests
