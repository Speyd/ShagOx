$ErrorActionPreference = "Stop"

Write-Host "=== PostgreSQL Replica Initialization ==="

# ============================================================
# Paths
# ============================================================

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

# ============================================================
# Environment
# ============================================================

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

# ============================================================
# Replicas
# ============================================================

$network = "postgres-replication_postgres-network"

$replicas = @(
    @{
        Name = "postgres-replica-1"
        Volume = "postgres-replication_replica_1_data"
    },
    @{
        Name = "postgres-replica-2"
        Volume = "postgres-replication_replica_2_data"
    },
    @{
        Name = "postgres-replica-3"
        Volume = "postgres-replication_replica_3_data"
    }
)

# ============================================================
# Start primary
# ============================================================

Write-Host ""
Write-Host "=== Starting primary ==="

docker compose @composeArgs up -d postgres-primary

if ($LASTEXITCODE -ne 0) {
    throw "Failed to start postgres-primary"
}

# ============================================================
# Wait for primary
# ============================================================

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

# ============================================================
# Check replication user
# ============================================================

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

# ============================================================
# Check replica volumes
# ============================================================

Write-Host ""
Write-Host "=== Checking replica volumes ==="

foreach ($replica in $replicas) {

    Write-Host "Checking $($replica.Name)..."

    $volumeCheck = docker run --rm `
        -v "$($replica.Volume):/var/lib/postgresql/data" `
        postgres:17 `
        bash -c "find /var/lib/postgresql/data -mindepth 1 -maxdepth 1 | head -n 1"

    if (-not [string]::IsNullOrWhiteSpace($volumeCheck)) {
        throw "Volume '$($replica.Volume)' is not empty. Remove it before running bootstrap."
    }
}

Write-Host "All replica volumes are empty."

# ============================================================
# Run pg_basebackup
# ============================================================

function Initialize-Replica {
    param (
        [string]$Name,
        [string]$Volume
    )

    Write-Host ""
    Write-Host "=== Initializing $Name ==="

    docker run --rm `
        --network $network `
        -e "PGPASSWORD=$replicatorPassword" `
        -v "${Volume}:/var/lib/postgresql/data" `
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
        throw "pg_basebackup failed for $Name"
    }

    Write-Host "$Name base backup completed."
}

foreach ($replica in $replicas) {

    Initialize-Replica `
        -Name $replica.Name `
        -Volume $replica.Volume
}

Write-Host ""
Write-Host "=== All base backups completed ==="

# ============================================================
# Start replicas
# ============================================================

Write-Host ""
Write-Host "=== Starting replicas ==="

foreach ($replica in $replicas) {

    Write-Host "Starting $($replica.Name)..."

    docker compose @composeArgs up -d $replica.Name

    if ($LASTEXITCODE -ne 0) {
        throw "Failed to start $($replica.Name)"
    }
}

# ============================================================
# Check replicas
# ============================================================

Write-Host ""
Write-Host "=== Checking replicas ==="

Start-Sleep -Seconds 3

foreach ($replica in $replicas) {

    Write-Host "Checking $($replica.Name)..."

    $recovery = docker exec `
        $replica.Name `
        psql `
        -U $postgresUser `
        -d $postgresDb `
        -tAc "SELECT pg_is_in_recovery();"

    if ($LASTEXITCODE -ne 0) {
        throw "Failed to check $($replica.Name)"
    }

    if ($recovery.Trim() -ne "t") {
        throw "$($replica.Name) is not in recovery mode."
    }

    Write-Host "$($replica.Name) is running as replica."
}

Write-Host ""
Write-Host "=== Replica initialization completed ==="