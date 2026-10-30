$ErrorActionPreference = "Stop"

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
}
finally {
    Pop-Location
}

Write-Host "=== Redis started ==="