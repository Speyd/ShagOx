# ShagOx

ShagOx is a web platform for creating, browsing, and managing listings.

Users can create listings, search for offers, add listings to favorites, view user profiles, and contact sellers.

> **Status:** MVP / Active Development

---

## Features

### Users

* Registration and authentication using JWT
* Browse and search listings
* Create, edit, and manage personal listings
* Add listings to favorites
* View user profiles
* Contact sellers

### Admin

* Manage users
* Manage listings

---

## Tech Stack

### Frontend

* React
* TypeScript
* npm

### Backend

* ASP.NET Core Web API
* REST API
* JWT Authentication
* Role-Based Access Control (RBAC)

### Infrastructure

* Docker
* Docker Compose
* Redis
* PostgreSQL
* PostgreSQL streaming replication

---

## Architecture

ShagOx uses a client-server architecture:

```text
                    ┌─────────────────┐
                    │    Frontend     │
                    │ React + TS      │
                    └────────┬────────┘
                             │
                             │ HTTP / REST
                             ▼
                    ┌─────────────────┐
                    │     Backend     │
                    │ ASP.NET Core API│
                    └───────┬─────────┘
                            │
                ┌───────────┴───────────┐
                │                       │
                ▼                       ▼
        ┌───────────────┐       ┌───────────────┐
        │ PostgreSQL    │       │     Redis     │
        │    Primary    │       │               │
        └───────┬───────┘       └───────────────┘
                │
                │ Streaming Replication
                ▼
        ┌───────────────┐
        │ PostgreSQL    │
        │    Replica    │
        └───────────────┘
```

The PostgreSQL replica is initialized from the primary using `pg_basebackup`.

---

# Getting Started

## Prerequisites

Before starting the project, make sure the following tools are installed:

* [Git](https://git-scm.com/)
* [Docker](https://www.docker.com/)
* Docker Compose
* .NET SDK 7+
* Node.js 18+
* npm

### Verify installation

```bash
git --version
docker --version
docker compose version
dotnet --version
node --version
npm --version
```

---

# 1. Clone the repository

```bash
git clone <repository-url>
cd ShagOx
```

---

# 2. Configure environment variables

Create the environment file from the provided example:

```powershell
Copy-Item .env.example .env
```

Then open `.env` and configure the required values.

Example:

```env
# PostgreSQL
POSTGRES_USER=postgres
POSTGRES_PASSWORD=your_password
POSTGRES_DB=shagox

# PostgreSQL replication
REPLICATOR_USER=replicator
REPLICATOR_PASSWORD=your_replication_password

# PostgreSQL ports
POSTGRES_PRIMARY_PORT=5432
POSTGRES_REPLICA_PORT=5433

# Redis
REDIS_PASSWORD=your_redis_password

# JWT
JWT_SECRET=your_jwt_secret
```

> Never commit `.env` or any file containing real credentials to the repository.

---

# 3. Start the infrastructure

ShagOx provides a root-level setup script that starts the required infrastructure in the correct order.

On Windows / PowerShell:

```powershell
.\setup.ps1
```

The script starts:

1. Redis
2. PostgreSQL Primary
3. PostgreSQL replication
4. PostgreSQL Replica

You do **not** need to manually start the PostgreSQL primary and replica.

The PostgreSQL replication initialization is handled automatically by:

```text
postgres-replication/replica/init-replica.ps1
```

The script performs the following steps:

```text
Start PostgreSQL Primary
        ↓
Wait until Primary is ready
        ↓
Verify replication user
        ↓
Verify replica volume
        ↓
Run pg_basebackup
        ↓
Start PostgreSQL Replica
        ↓
Verify replication mode
```

After successful execution, the infrastructure should be ready.

---

# 4. Verify Docker containers

Check the running containers:

```bash
docker ps
```

You should see containers similar to:

```text
redis
postgres-primary
postgres-replica
```

You can also check their status with:

```bash
docker compose ps
```

> PostgreSQL and Redis are managed by separate Docker Compose projects in the repository.

---

# 5. Start the Backend

Navigate to the backend directory:

```bash
cd backend
```

Restore dependencies:

```bash
dotnet restore
```

Start the API:

```bash
dotnet run
```

The API will be available at the URL displayed by ASP.NET Core.

For example:

```text
http://localhost:5000
```

or:

```text
https://localhost:7000
```

---

# 6. Start the Frontend

Open a new terminal and navigate to the frontend:

```bash
cd frontend
```

Install dependencies:

```bash
npm install
```

Start the development server:

```bash
npm run dev
```

The frontend will usually be available at:

```text
http://localhost:5173
```

---

# 7. Running the complete application

For a normal development session, use the following terminals.

### Terminal 1 — Infrastructure

From the project root:

```powershell
.\setup.ps1
```

### Terminal 2 — Backend

```bash
cd backend
dotnet run
```

### Terminal 3 — Frontend

```bash
cd frontend
npm install
npm run dev
```

Once all three are running:

```text
Browser
   │
   ▼
Frontend
   │
   ▼
ASP.NET Core API
   │
   ├── PostgreSQL Primary
   │
   └── Redis
```

---

# Stopping the infrastructure

To stop Redis:

```bash
cd infrastructure/my_redis
docker compose down
```

To stop PostgreSQL:

```bash
cd postgres-replication/replica
docker compose down
```

---

## Removing Docker volumes

If you need to completely reset the local infrastructure:

```bash
docker compose down -v
```

> **Warning:** Removing Docker volumes permanently deletes the local PostgreSQL and Redis data stored in those volumes.

For PostgreSQL replication, a full reset may also require removing the replica volume before running `init-replica.ps1` again.

---

# PostgreSQL Replication

ShagOx uses PostgreSQL Primary/Replica replication.

The primary database accepts writes:

```text
Application
     │
     ▼
PostgreSQL Primary
     │
     │ WAL / Streaming Replication
     ▼
PostgreSQL Replica
```

The replica is initialized using:

```bash
pg_basebackup
```

The replication setup is automated by:

```text
postgres-replication/replica/init-replica.ps1
```

You should normally **not** run `pg_basebackup` manually.

### Important

The replica must have an empty data directory when the replication bootstrap is performed for the first time.

If the bootstrap script reports that the replica volume is not empty, make sure there is no existing replica data before attempting to initialize it again.

---

# Redis

Redis is configured as part of the infrastructure.

The Redis setup includes:

* Redis configuration
* Persistent data
* TLS certificates
* ACL configuration

Redis initialization is performed automatically before the Redis server starts.

The Redis infrastructure is located in:

```text
infrastructure/
└── my_redis/
```

---

# Environment Variables

The project uses environment variables for configuration and secrets.

At minimum, configure:

### PostgreSQL

```env
POSTGRES_USER=
POSTGRES_PASSWORD=
POSTGRES_DB=
```

### PostgreSQL Replication

```env
REPLICATOR_USER=
REPLICATOR_PASSWORD=
```

### PostgreSQL Ports

```env
POSTGRES_PRIMARY_PORT=
POSTGRES_REPLICA_PORT=
```

### Redis

```env
REDIS_PASSWORD=
```

### Authentication

```env
JWT_SECRET=
```

The exact variables required by the current application may change during development.

Use `.env.example` as the source of truth.

---

# API

The API is exposed by the ASP.NET Core backend.

### Authentication

```http
POST /api/auth/register
POST /api/auth/login
```

### Listings

```http
GET  /api/listings
POST /api/listings
```

Additional endpoints are available in the backend project.

---

# Project Structure

```text
ShagOx/
│
├── backend/
│   └── ...
│
├── frontend/
│   └── ...
│
├── infrastructure/
│   └── my_redis/
│       ├── docker-compose.yaml
│       ├── redis/
│       └── init/
│
├── postgres-replication/
│   ├── docker-compose.yaml
│   │
│   ├── primary/
│   │   └── init/
│   │       └── ...
│   │
│   └── replica/
│       └── init-replica.ps1
│
├── .env.example
├── .gitignore
├── setup.ps1
└── README.md
```

---

# Troubleshooting

## Docker containers are not starting

Check the container status:

```bash
docker ps -a
```

Check logs:

```bash
docker logs <container-name>
```

For example:

```bash
docker logs postgres-primary
docker logs postgres-replica
docker logs redis
```

---

## PostgreSQL replica initialization fails

Check the primary logs:

```bash
docker logs postgres-primary
```

Make sure:

* PostgreSQL Primary is running
* The replication user exists
* `REPLICATOR_USER` and `REPLICATOR_PASSWORD` are correct
* The replica volume is empty
* Docker networking is available

Then run the initialization script again:

```powershell
cd postgres-replication\replica
.\init-replica.ps1
```

---

## Redis fails to start

Check Redis logs:

```bash
docker logs redis
```

If Redis initialization fails, check the output of the initialization container:

```bash
docker logs redis-init
```

---

# Development Notes

This project is currently under active development.

The architecture and configuration may change as new features are introduced.

The current setup is intended primarily for local development and testing.

---

# License

License information will be added later.