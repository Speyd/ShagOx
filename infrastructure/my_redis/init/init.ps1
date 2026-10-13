$ErrorActionPreference = "Stop"

$SCRIPT_DIR = $PSScriptRoot
$ProjectRoot = (Resolve-Path (Join-Path $SCRIPT_DIR "..\..\..")).Path
$envFile = Join-Path $ProjectRoot ".env"

if (Test-Path -LiteralPath $envFile -PathType Leaf) {
    Get-Content -LiteralPath $envFile | ForEach-Object {
        $line = $_.Trim()

        if (-not $line -or $line.StartsWith("#")) {
            return
        }

        if ($line.Contains("=")) {
            $key, $value = $line -split "=", 2

            [Environment]::SetEnvironmentVariable(
                $key.Trim(),
                $value.Trim(),
                "Process"
            )
        }
        else {
            Write-Warning "String miss: $line"
        }
}
    }
    else {
    throw "File .env not found: $envFile"
}



Write-Host "========================================"
Write-Host " Redis infrastructure initialization"
Write-Host "========================================"
Write-Host ""

Write-Host "[1/3] Initializing certificates..."
& (Join-Path $SCRIPT_DIR "init-certs.ps1")
if ($LASTEXITCODE -ne 0 -and $null -ne $LASTEXITCODE) {
    throw "Certificate initialization failed"
}

Write-Host ""

Write-Host "[2/3] Initializing main Redis..."
& (Join-Path $SCRIPT_DIR "../redis/init-redis.ps1")
if ($LASTEXITCODE -ne 0 -and $null -ne $LASTEXITCODE) {
    throw "Main Redis initialization failed"
}
Write-Host ""

Write-Host "[3/3] Initializing User Redis..."

& (Join-Path $SCRIPT_DIR "../user-redis/init-user-redis.ps1")
if ($LASTEXITCODE -ne 0 -and $null -ne $LASTEXITCODE) {
    throw "User Redis initialization failed"
}

Write-Host ""

Write-Host "========================================"
Write-Host " Redis infrastructure initialized"
Write-Host "========================================"

Write-Host "Starting Docker Compose..."

$DockerRoot = (Resolve-Path (Join-Path $SCRIPT_DIR "..\")).Path
Push-Location $DockerRoot
try {
    docker compose up -d

    if ($LASTEXITCODE -ne 0) {
        throw "Docker Compose startup failed"
    }
}
finally {
    Pop-Location
}

Write-Host ""
Write-Host "Docker Compose started successfully."