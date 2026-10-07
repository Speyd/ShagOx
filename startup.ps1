$ErrorActionPreference = "Stop"

Write-Host "=== Starting Redis Server ==="

Push-Location infrastructure\my_redis\scripts
./redis-cli.ps1
Pop-Location


Write-Host "=== Server started ==="