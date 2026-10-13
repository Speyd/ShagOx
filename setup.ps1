$ErrorActionPreference = "Stop"

Write-Host "=== Starting infrastructure ==="

Push-Location infrastructure\my_redis\init
.\init.ps1
Pop-Location

Write-Host "=== Starting PostgreSQL replication ==="

Push-Location postgres-replication\replica
.\init-replica.ps1
Pop-Location

Write-Host "=== Server started ==="