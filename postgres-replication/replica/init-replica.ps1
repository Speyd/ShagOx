$ErrorActionPreference = "Stop"

Write-Host "=== PostgreSQL Replica Initialization ==="

$scriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path

$projectRoot = Resolve-Path (Join-Path $scriptDirectory "../..")
$postgresRoot = Resolve-Path (Join-Path $scriptDirectory "..")
$envFile = Join-Path $projectRoot ".env"
$composeFile = Join-Path $postgresRoot "docker-compose.yml"

if (-not (Test-Path $envFile)) {
    throw ".env not found: $envFile"
}

if (-not (Test-Path $composeFile)) {
    throw "docker-compose.yml not found: $composeFile"
}

Write-Host "Project root: $projectRoot"
Write-Host "Env file:     $envFile"
Write-Host "Compose file: $composeFile"

$composeArgs = @(
    "--env-file", $envFile
    "-f", $composeFile
)

function Get-EnvValue {
    param (
        [string]$Name
    )

    $line = Get-Content $envFile |
        Where-Object { $_ -match "^$Name=" } |
        Select-Object -First 1

    if (-not $line) {
        throw "$Name not found in .env"
    }

    return ($line -replace "^$Name=", "").Trim()
}

$postgresPassword = Get-EnvValue "POSTGRES_PASSWORD"
$postgresUser = Get-EnvValue "POSTGRES_USER"
$postgresDb = Get-EnvValue "POSTGRES_DB"
$replicatorUser = Get-EnvValue "REPLICATOR_USER"
$replicatorPassword = Get-EnvValue "REPLICATOR_PASSWORD"

$network = "postgres-replication_postgres-network"
$replicaVolume = "postgres-replication_replica_data"

Write-Host ""
Write-Host "=== Starting primary ==="

docker compose @composeArgs up -d postgres-primary

if ($LASTEXITCODE -ne 0) {
    throw "Failed to start postgres-primary"
}

Write-Host ""
Write-Host "=== Waiting for primary ==="

do {
    docker exec `
        -e "PGPASSWORD=$postgresPassword" `
        postgres-primary `
        pg_isready `
        -U $postgresUser `
        -d $postgresDb `
        2>$null

    if ($LASTEXITCODE -ne 0) {
        Start-Sleep -Seconds 2
        Write-Host "Waiting..."
    }
}
while ($LASTEXITCODE -ne 0)

Write-Host "Primary is ready."

Write-Host ""
Write-Host "=== Checking replication user ==="

$roleExists = docker exec `
    -e "PGPASSWORD=$postgresPassword" `
    postgres-primary `
    psql `
    -U $postgresUser `
    -d $postgresDb `
    -tAc "SELECT 1 FROM pg_roles WHERE rolname = '$replicatorUser';"

if ($roleExists.Trim() -ne "1") {
    throw "Replication user '$replicatorUser' does not exist."
}

Write-Host "Replication user exists."

Write-Host ""
Write-Host "=== Checking replica volume ==="

$volumeCheck = docker run --rm `
    -v "${replicaVolume}:/var/lib/postgresql/data" `
    postgres:17 `
    bash -c "find /var/lib/postgresql/data -mindepth 1 -maxdepth 1 | head -n 1"

if (-not [string]::IsNullOrWhiteSpace($volumeCheck)) {
    throw "Replica volume is not empty. Remove it before running bootstrap."
}

Write-Host "Replica volume is empty."

Write-Host ""
Write-Host "=== Running pg_basebackup ==="

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
    throw "pg_basebackup failed"
}

Write-Host ""
Write-Host "=== Base backup completed ==="

Write-Host ""
Write-Host "=== Starting replica ==="

docker compose @composeArgs up -d postgres-replica

if ($LASTEXITCODE -ne 0) {
    throw "Failed to start postgres-replica"
}

Start-Sleep -Seconds 3

Write-Host ""
Write-Host "=== Checking replica ==="

docker exec `
    postgres-replica `
    psql `
    -U $postgresUser `
    -d $postgresDb `
    -c "SELECT pg_is_in_recovery();"

Write-Host ""
Write-Host "=== Replica initialization completed ==="