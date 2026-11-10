$ErrorActionPreference = "Stop"

$projectRoot = Split-Path -Parent $PSScriptRoot

Write-Host "=== Starting Redis Server ==="

Push-Location (Join-Path $projectRoot "infrastructure\my_redis\scripts")
try {
    .\redis-cli.ps1

    if (-not $?) {
        throw "Redis CLI execution failed."
    }
}
finally {
    Pop-Location
}


Write-Host "=== Server started ==="