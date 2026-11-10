$ErrorActionPreference = "Stop"


$projectRoot = Split-Path -Parent $PSScriptRoot

$envFile = Join-Path $projectRoot ".env"
$initDirectory = Join-Path $projectRoot "postgres-replication/primary/init"

$container = "postgres-primary"


if (-not (Test-Path $envFile)) {
    throw ".env not found: $envFile"
}

if (-not (Test-Path $initDirectory)) {
    throw "Init directory not found: $initDirectory"
}


function Get-EnvValue {
    param([string]$Name)

    $line = Get-Content $envFile |
        Where-Object { $_ -match "^\s*$Name=" } |
        Select-Object -First 1

    if (-not $line) {
        throw "$Name not found in .env"
    }

    return ($line -replace "^\s*$Name=", "").Trim().Trim('"').Trim("'")
}

$postgresUser      = Get-EnvValue "POSTGRES_USER"
$postgresPassword  = Get-EnvValue "POSTGRES_PASSWORD"
$postgresDb        = Get-EnvValue "POSTGRES_DB"
$replicatorUser    = Get-EnvValue "REPLICATOR_USER"
$replicatorPassword = Get-EnvValue "REPLICATOR_PASSWORD"
$pgbouncerUser     = Get-EnvValue "PGBOUNCER_USER"
$pgbouncerPassword = Get-EnvValue "PGBOUNCER_PASSWORD"


$initScripts = @(
    "01-init.sh",
    "02-init-pgbouncer.sh"
)

foreach ($script in $initScripts) {

    $scriptPath = Join-Path $initDirectory $script

    if (-not (Test-Path $scriptPath)) {
        throw "Script not found: $scriptPath"
    }

    Write-Host ""
    Write-Host "===================================="
    Write-Host "Running $script"
    Write-Host "===================================="

    $scriptContent = Get-Content $scriptPath -Raw

    $scriptContent | docker exec -i `
        -e "POSTGRES_USER=$postgresUser" `
        -e "POSTGRES_PASSWORD=$postgresPassword" `
        -e "POSTGRES_DB=$postgresDb" `
        -e "PGPASSWORD=$postgresPassword" `
        -e "REPLICATOR_USER=$replicatorUser" `
        -e "REPLICATOR_PASSWORD=$replicatorPassword" `
        -e "PGBOUNCER_USER=$pgbouncerUser" `
        -e "PGBOUNCER_PASSWORD=$pgbouncerPassword" `
        $container bash -s

    if ($LASTEXITCODE -ne 0) {
        throw "$script failed with exit code $LASTEXITCODE"
    }

    Write-Host "$script completed successfully." -ForegroundColor Green
}

Write-Host ""
Write-Host "===================================="
Write-Host "All initialization scripts completed successfully!"
Write-Host "====================================" -ForegroundColor Green