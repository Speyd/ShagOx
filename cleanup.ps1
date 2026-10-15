param(
    [switch]$RemoveVolumes,
    [switch]$ReplicaVolumesOnly
)

$ErrorActionPreference = "Stop"

if ($RemoveVolumes -and $ReplicaVolumesOnly) {
    throw "Use either -RemoveVolumes or -ReplicaVolumesOnly, not both."
}

# ============================================================
# Explicit allowlists
# ============================================================

$allContainers = @(
    "postgres-primary"
    "postgres-replica-1"
    "postgres-replica-2"
    "postgres-replica-3"
    "postgres-replication"
    "redis-init"
    "redis"
    "my_redis"
)

$replicaContainers = @(
    "postgres-replica-1"
    "postgres-replica-2"
    "postgres-replica-3"
)

$replicaVolumes = @(
    "postgres-replication_replica_1_data"
    "postgres-replication_replica_2_data"
    "postgres-replication_replica_3_data"
)

$allVolumes = @(
    "postgres-replication_primary_data"
    "postgres-replication_replica_1_data"
    "postgres-replication_replica_2_data"
    "postgres-replication_replica_3_data"
    "my_redis_redis-data"
    "my_redis_user-redis-data"
)

$knownNetworks = @(
    "postgres-replication_postgres-network"
    "my_redis_default"
)

# ============================================================
# Select cleanup scope
# ============================================================

if ($ReplicaVolumesOnly) {
    $containersToRemove = $replicaContainers
    $volumesToRemove = $replicaVolumes
}
else {
    $containersToRemove = $allContainers

    if ($RemoveVolumes) {
        $volumesToRemove = $allVolumes
    }
    else {
        $volumesToRemove = @()
    }
}

# ============================================================
# Check Docker
# ============================================================

Write-Host "=== Checking Docker ===" -ForegroundColor Cyan

& docker info *> $null

if ($LASTEXITCODE -ne 0) {
    throw "Docker Engine is not available."
}

# ============================================================
# Preview and confirmation
# ============================================================

Write-Host ""
Write-Host "Containers to remove:" -ForegroundColor Yellow
$containersToRemove | ForEach-Object { Write-Host "  $_" }

if ($volumesToRemove.Count -gt 0) {
    Write-Host ""
    Write-Host "Volumes to remove:" -ForegroundColor Yellow
    $volumesToRemove | ForEach-Object { Write-Host "  $_" }
}

if ($ReplicaVolumesOnly) {
    Write-Host ""
    Write-Host "Only PostgreSQL replicas will be removed." -ForegroundColor Yellow
}
elseif ($RemoveVolumes) {
    Write-Host ""
    Write-Host "WARNING: All listed PostgreSQL and Redis data volumes will be deleted!" `
        -ForegroundColor Red
}
else {
    Write-Host ""
    Write-Host "Volumes will be preserved." -ForegroundColor Green
}

$confirmation = Read-Host "Type YES to continue"

if ($confirmation -cne "YES") {
    Write-Host "Cancelled." -ForegroundColor Yellow
    return
}

# ============================================================
# Remove containers
# ============================================================

Write-Host ""
Write-Host "=== Removing containers ===" -ForegroundColor Cyan

foreach ($name in $containersToRemove) {
    & docker container rm --force $name 2>$null

    if ($LASTEXITCODE -eq 0) {
        Write-Host "Removed container: $name" -ForegroundColor Green
    }
    else {
        Write-Host "Skipped missing or unavailable container: $name" `
            -ForegroundColor DarkGray
    }
}

# ============================================================
# Remove volumes from explicit allowlist
# ============================================================

if ($volumesToRemove.Count -gt 0) {
    Write-Host ""
    Write-Host "=== Removing volumes ===" -ForegroundColor Cyan

    $existingVolumes = @(& docker volume ls --quiet)

    if ($LASTEXITCODE -ne 0) {
        throw "Failed to list Docker volumes."
    }

    foreach ($name in $volumesToRemove) {
        if ($name -notin $existingVolumes) {
            Write-Host "Volume not found: $name" -ForegroundColor DarkGray
            continue
        }

        & docker volume rm $name

        if ($LASTEXITCODE -eq 0) {
            Write-Host "Removed volume: $name" -ForegroundColor Green
        }
        else {
            Write-Warning "Could not remove volume '$name'. It may still be in use."
        }
    }
}

# ============================================================
# Remove known networks, except in replica-only mode
# ============================================================

if (-not $ReplicaVolumesOnly) {
    Write-Host ""
    Write-Host "=== Removing known networks ===" -ForegroundColor Cyan

    $existingNetworks = @(& docker network ls --format "{{.Name}}")

    if ($LASTEXITCODE -ne 0) {
        Write-Warning "Could not list Docker networks."
    }
    else {
        foreach ($name in $knownNetworks) {
            if ($name -notin $existingNetworks) {
                continue
            }

            & docker network rm $name 2>$null

            if ($LASTEXITCODE -eq 0) {
                Write-Host "Removed network: $name" -ForegroundColor Green
            }
            else {
                Write-Host "Network kept (possibly in use): $name" `
                    -ForegroundColor DarkGray
            }
        }
    }
}

Write-Host ""
Write-Host "=== Cleanup completed ===" -ForegroundColor Green