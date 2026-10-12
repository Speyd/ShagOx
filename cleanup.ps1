param(
    [switch]$RemoveVolumes,
    [switch]$ReplicaVolumesOnly
)

$ErrorActionPreference = "Stop"

$PSNativeCommandUseErrorActionPreference = $false

if ($RemoveVolumes -and $ReplicaVolumesOnly) {
    throw "Use either -RemoveVolumes or -ReplicaVolumesOnly, not both."
}

# ============================================================
# Containers
# ============================================================

$allContainerNames = @(
    "postgres-primary",
    "postgres-replica-1",
    "postgres-replica-2",
    "postgres-replica-3",
    "postgres-replication",
    "redis-init",
    "redis",
    "my_redis"
)

$replicaContainerNames = @(
    "postgres-replica-1",
    "postgres-replica-2",
    "postgres-replica-3"
)

if ($ReplicaVolumesOnly) {
    $containerNames = $replicaContainerNames
}
else {
    $containerNames = $allContainerNames
}

# ============================================================
# Explicit volume allowlist
# ============================================================


$replicaVolumeNames = @(
    "postgres-replication_replica_1_data",
    "postgres-replication_replica_2_data",
    "postgres-replication_replica_3_data"
)

$allVolumeNames = @(
    "postgres-replication_primary_data"

    "postgres-replication_replica_1_data",
    "postgres-replication_replica_2_data",
    "postgres-replication_replica_3_data"

    "my_redis_redis-data",
    "my_redis_redis-certs-data",
    "my_redis_redis-acl-data"
)

# ============================================================
# Networks
# ============================================================

$knownNetworkNames = @(
    "postgres-replication_postgres-network",
    "my_redis_default"
)

# ============================================================
# Check Docker
# ============================================================

Write-Host "=== Checking Docker ===" -ForegroundColor Cyan

& docker info *> $null

if ($LASTEXITCODE -ne 0) {
    throw "Docker Engine is not available."
}

# ============================================================
# Get existing containers
# ============================================================

$existingContainerNames = @(
    & docker container ls -a --format "{{.Names}}"
)

if ($LASTEXITCODE -ne 0) {
    throw "Failed to list Docker containers."
}

# ============================================================
# Select volumes
# ============================================================

$volumesToRemove = @()

if ($RemoveVolumes -or $ReplicaVolumesOnly) {

    Write-Host ""
    Write-Host "=== Checking volumes ===" -ForegroundColor Cyan

    $existingVolumeNames = @(
        & docker volume ls --quiet
    )

    if ($LASTEXITCODE -ne 0) {
        throw "Failed to list Docker volumes."
    }

    if ($ReplicaVolumesOnly) {
        $allowedVolumes = $replicaVolumeNames
    }
    else {
        $allowedVolumes = $allVolumeNames
    }

    $volumesToRemove = @(
        $allowedVolumes |
            Where-Object {
                $_ -in $existingVolumeNames
            } |
            Sort-Object -Unique
    )

    if ($volumesToRemove.Count -eq 0) {
        Write-Host "No matching volumes found." -ForegroundColor DarkGray
    }
    else {
        Write-Host "Volumes selected for deletion:" -ForegroundColor Yellow

        foreach ($volumeName in $volumesToRemove) {
            Write-Host "  $volumeName"
        }
    }

    Write-Host ""

    if ($ReplicaVolumesOnly) {
        Write-Host `
            "WARNING: Only replica data will be deleted. Primary and Redis volumes are preserved." `
            -ForegroundColor Yellow
    }
    else {
        Write-Host `
            "WARNING: PostgreSQL Primary, replica and Redis data may be permanently deleted." `
            -ForegroundColor Red
    }

    $confirmation = Read-Host "Type YES to continue"

    if ($confirmation -cne "YES") {
        Write-Host "Cancelled." -ForegroundColor Yellow
        return
    }
}

# ============================================================
# Remove containers
# ============================================================

Write-Host ""
Write-Host "=== Removing containers ===" -ForegroundColor Cyan

foreach ($name in $containerNames) {

    if ($name -notin $existingContainerNames) {
        Write-Host "Container not found, skipping: $name" `
            -ForegroundColor DarkGray

        continue
    }

    Write-Host "Removing container: $name" -ForegroundColor Cyan

    $null = & docker container rm --force $name 2>$null

    if ($LASTEXITCODE -ne 0) {
        Write-Warning "Could not remove container: $name"
    }
    else {
        Write-Host "Removed: $name" -ForegroundColor Green
    }
}

# ============================================================
# Remove selected volumes
# ============================================================

if ($RemoveVolumes -or $ReplicaVolumesOnly) {

    Write-Host ""
    Write-Host "=== Removing volumes ===" -ForegroundColor Cyan

    foreach ($volumeName in $volumesToRemove) {

        Write-Host "Removing volume: $volumeName" -ForegroundColor Yellow

        $null = & docker volume rm $volumeName 2>$null

        if ($LASTEXITCODE -ne 0) {
            Write-Warning `
                "Could not remove volume '$volumeName'. It may still be in use."
        }
        else {
            Write-Host "Removed volume: $volumeName" -ForegroundColor Green
        }
    }
}

# ============================================================
# Remove known networks
# ============================================================

if (-not $ReplicaVolumesOnly) {

    Write-Host ""
    Write-Host "=== Removing networks ===" -ForegroundColor Cyan

    $existingNetworkNames = @(
        & docker network ls --format "{{.Name}}"
    )

    if ($LASTEXITCODE -ne 0) {
        Write-Warning "Could not list Docker networks."
    }
    else {
        foreach ($networkName in $knownNetworkNames) {

            if ($networkName -notin $existingNetworkNames) {
                continue
            }

            Write-Host "Removing network: $networkName" -ForegroundColor Cyan

            $null = & docker network rm $networkName 2>$null

            if ($LASTEXITCODE -ne 0) {
                Write-Host `
                    "Network kept, possibly still in use: $networkName" `
                    -ForegroundColor DarkGray
            }
            else {
                Write-Host "Removed network: $networkName" `
                    -ForegroundColor Green
            }
        }
    }
}

Write-Host ""
Write-Host "=== Cleanup completed ===" -ForegroundColor Green