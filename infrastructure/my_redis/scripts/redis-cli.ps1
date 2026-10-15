$ErrorActionPreference = "Stop"

# ============================================================
# Paths
# ============================================================

$ProjectRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$EnvRoot = Resolve-Path (Join-Path $PSScriptRoot "../../..")

$CertsPath = Join-Path $ProjectRoot "certs"

$CaPath = Join-Path $CertsPath "ca"
$ClientPath = Join-Path $CertsPath "client"

$EnvFile = Join-Path $EnvRoot ".env"


# ============================================================
# Read .env values
# ============================================================

if (-not (Test-Path $EnvFile)) {
    throw ".env file not found: $EnvFile"
}

function Get-EnvValue {
    param (
        [Parameter(Mandatory = $true)]
        [string]$Name
    )

    $line = Get-Content $EnvFile |
        Where-Object { $_ -match "^\s*$([regex]::Escape($Name))\s*=" } |
        Select-Object -First 1

    if (-not $line) {
        return $null
    }

    $value = $line -replace "^\s*$([regex]::Escape($Name))\s*=\s*", ""

    if (
        ($value.StartsWith('"') -and $value.EndsWith('"')) -or
        ($value.StartsWith("'") -and $value.EndsWith("'"))
    ) {
        $value = $value.Substring(1, $value.Length - 2)
    }

    return $value
}


# ============================================================
# Redis credentials
# ============================================================

$RedisDataUser = Get-EnvValue "REDIS_DATA_USER"
$RedisDataPassword = Get-EnvValue "REDIS_DATA_PASSWORD"

$RedisUser = Get-EnvValue "REDIS_USER"
$RedisUserPassword = Get-EnvValue "REDIS_USER_PASSWORD"


if ([string]::IsNullOrWhiteSpace($RedisDataUser)) {
    throw "REDIS_DATA_USER is missing in .env"
}

if ([string]::IsNullOrWhiteSpace($RedisDataPassword)) {
    throw "REDIS_DATA_PASSWORD is missing in .env"
}

if ([string]::IsNullOrWhiteSpace($RedisUser)) {
    throw "REDIS_USER is missing in .env"
}

if ([string]::IsNullOrWhiteSpace($RedisUserPassword)) {
    throw "REDIS_USER_PASSWORD is missing in .env"
}


# ============================================================
# Verify local certificates
# ============================================================

$RequiredCertificates = @(
    (Join-Path $CaPath "ca.crt"),
    (Join-Path $ClientPath "client.crt"),
    (Join-Path $ClientPath "client.key")
)

foreach ($Certificate in $RequiredCertificates) {
    if (-not (Test-Path -LiteralPath $Certificate -PathType Leaf)) {
        throw "Certificate file not found: $Certificate"
    }
}


Write-Host ""
Write-Host "Certificates found:" -ForegroundColor Green
Write-Host ""
Write-Host "  $(Join-Path $CaPath 'ca.crt')"
Write-Host "  $(Join-Path $ClientPath 'client.crt')"
Write-Host "  $(Join-Path $ClientPath 'client.key')"
Write-Host ""


# ============================================================
# Start Redis CLI in a separate PowerShell window
# ============================================================

function Start-RedisCliWindow {
    param (
        [Parameter(Mandatory = $true)]
        [string]$Title,

        [Parameter(Mandatory = $true)]
        [string]$Container,

        [Parameter(Mandatory = $true)]
        [string]$User,

        [Parameter(Mandatory = $true)]
        [string]$Password
    )

    $SafeTitle = $Title.Replace("'", "''")
    $SafeUser = $User.Replace("'", "''")
    $SafePassword = $Password.Replace("'", "''")

    $command = @"
`$Host.UI.RawUI.WindowTitle = '$SafeTitle'

docker exec -e 'REDISCLI_AUTH=$SafePassword' -it $Container redis-cli --tls --cacert /certs/ca/ca.crt --cert /certs/client/client.crt --key /certs/client/client.key --user '$SafeUser'

if (`$LASTEXITCODE -ne 0) {
    Write-Host "Redis CLI exited with an error." -ForegroundColor Red
}
"@

    $encoded = [Convert]::ToBase64String(
        [System.Text.Encoding]::Unicode.GetBytes($command)
    )

    $shell = (Get-Process -Id $PID).Path

    Start-Process $shell -ArgumentList @(
        "-NoExit",
        "-EncodedCommand", $encoded
    )
}


# ============================================================
# Open connections
# ============================================================

Write-Host "Opening Redis CLI windows..." -ForegroundColor Cyan
Write-Host ""

Start-RedisCliWindow `
    -Title "Redis: $RedisDataUser" `
    -Container "redis" `
    -User $RedisDataUser `
    -Password $RedisDataPassword

Start-RedisCliWindow `
    -Title "Redis: $RedisUser" `
    -Container "user-redis" `
    -User $RedisUser `
    -Password $RedisUserPassword

Write-Host "Redis CLI windows started." -ForegroundColor Green