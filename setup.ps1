$ErrorActionPreference = "Stop"

$env:OPENSSL_CONF = ""

$env:OPENSSL_CONF = $null

Write-Host "=== Checking OpenSSL ==="

if (-not (Get-Command openssl -ErrorAction SilentlyContinue)) {
    Write-Host "OpenSSL not found. Installing..."

    winget install --id ShiningLight.OpenSSL.Light --exact `
        --accept-source-agreements `
        --accept-package-agreements

    if ($LASTEXITCODE -ne 0) {
        throw "Failed to install OpenSSL."
    }

    $env:Path = [Environment]::GetEnvironmentVariable("Path", "Machine") +
                ";" +
                [Environment]::GetEnvironmentVariable("Path", "User")
}

if (-not (Get-Command openssl -ErrorAction SilentlyContinue)) {
    throw "OpenSSL installation completed, but openssl.exe was not found in PATH. Restart PowerShell and try again."
}

Write-Host "OpenSSL is available."
openssl version

Write-Host "=== Starting infrastructure (Redis) ==="

Push-Location infrastructure\my_redis\init
try {
    .\init.ps1
    if (-not $?){
        throw "Redis initialization failed."
    }
}
finally {
    Pop-Location
}

Write-Host "=== Starting PostgreSQL replication ==="

Push-Location postgres-replication\replica
try {
    .\init-replica.ps1
    if (-not $?){
        throw "PostgreSQL replication initialization failed."
    }
}
finally {
    Pop-Location
}

Write-Host "=== Starting PgBiuncer user list ==="

Push-Location postgres-replication\pgbouncer\script
try {
    .\generate-pgbouncer-userlist.ps1
    if (-not $?){
        throw "PgBouncer user list initialization failed."
    }
}
finally {
    Pop-Location
}

Write-Host "=== Starting PgBouncer ==="

Push-Location postgres-replication
try {
    docker compose up -d --build pgbouncer

    if ($LASTEXITCODE -ne 0) {
        throw "PgBouncer container startup failed."
    }

    Write-Host "PgBouncer started successfully."
}
finally {
    Pop-Location
}

Write-Host "=== Server started ==="