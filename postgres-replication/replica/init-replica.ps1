$ErrorActionPreference = "Stop"

Write-Host "=== PostgreSQL Replica Initialization ==="

$envFile = ".env"

if (-not (Test-Path $envFile)) {
    Write-Error ".env not found"
    exit 1
}

function Get-EnvValue {
    param (
        [string]$Name
    )

    $line = Get-Content $envFile |
        Where-Object { $_ -match "^$Name=" } |
        Select-Object -First 1

    if (-not $line) {
        Write-Error "$Name not found in .env"
        exit 1
    }

    return ($line -replace "^$Name=", "").Trim()
}

$postgresUser = Get-EnvValue "POSTGRES_USER"
$postgresDb = Get-EnvValue "POSTGRES_DB"
$replicatorUser = Get-EnvValue "REPLICATOR_USER"
$replicatorPassword = Get-EnvValue "REPLICATOR_PASSWORD"
$primaryPort = Get-EnvValue "POSTGRES_PRIMARY_PORT"
$replicaPort = Get-EnvValue "POSTGRES_REPLICA_PORT"

Write-Host "Starting primary..."

docker compose up -d postgres-primary

Write-Host "Waiting for PostgreSQL..."

Start-Sleep -Seconds 5

Write-Host "Checking replicator role..."

docker exec postgres-primary psql `
    -U $postgresUser `
    -d $postgresDb `
    -c "SELECT rolname, rolreplication FROM pg_roles WHERE rolname = '$replicatorUser';"

Write-Host ""
Write-Host "Replica volume must be empty."

$replicaVolume = "postgres-replication_replica_data"
$network = "postgres-replication_postgres-network"

Write-Host "Running pg_basebackup..."

docker run --rm `
    --network $network `
    -e "PGPASSWORD=$replicatorPassword" `
    -v "${replicaVolume}:/var/lib/postgresql/data" `
    postgres:17 `
    pg_basebackup `
    -h postgres-primary `
    -U $replicatorUser `
    -D /var/lib/postgresql/data `
    -Fp `
    -Xs `
    -P `
    -R

if ($LASTEXITCODE -ne 0) {
    Write-Error "pg_basebackup failed"
    exit 1
}

Write-Host ""
Write-Host "Base backup completed."

Write-Host "Starting replica..."

docker compose up -d postgres-replica

Write-Host ""
Write-Host "Replica started."

Start-Sleep -Seconds 5

Write-Host "Checking recovery status..."

docker exec postgres-replica `
    psql `
    -U $postgresUser `
    -d $postgresDb `
    -c "SELECT pg_is_in_recovery();"

Write-Host ""
Write-Host "=== Done ==="