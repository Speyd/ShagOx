$ErrorActionPreference = "Stop"

$ProjectRoot = Resolve-Path (Join-Path $PSScriptRoot "..")

$CertsPath = Join-Path $ProjectRoot "certs"

$CaPath = Join-Path $CertsPath "ca"
$ClientPath = Join-Path $CertsPath "client"

$CertVolume = "my_redis_redis-certs-data"




$EnvFile = Join-Path $ProjectRoot ".env"

function Get-EnvValue {
    param (
        [string]$Name
    )

    $line = Get-Content $EnvFile |
        Where-Object { $_ -match "^\s*$Name\s*=" } |
        Select-Object -First 1

    if (-not $line) {
        return $null
    }

    $value = $line -replace "^\s*$Name\s*=\s*", ""

    if (
        ($value.StartsWith('"') -and $value.EndsWith('"')) -or
        ($value.StartsWith("'") -and $value.EndsWith("'"))
    ) {
        $value = $value.Substring(1, $value.Length - 2)
    }

    return $value
}

$RedisUser = Get-EnvValue "REDIS_USER"
$RedisPassword = Get-EnvValue "REDIS_PASSWORD"

if ([string]::IsNullOrWhiteSpace($RedisUser)) {
    throw "REDIS_USER is missing in .env"
}

if ([string]::IsNullOrWhiteSpace($RedisPassword)) {
    throw "REDIS_PASSWORD is missing in .env"
}




New-Item -ItemType Directory -Force $CaPath | Out-Null
New-Item -ItemType Directory -Force $ClientPath | Out-Null




Remove-Item `
    (Join-Path $CaPath "ca.crt") `
    -Force `
    -ErrorAction SilentlyContinue

Remove-Item `
    (Join-Path $ClientPath "client.crt") `
    -Force `
    -ErrorAction SilentlyContinue

Remove-Item `
    (Join-Path $ClientPath "client.key") `
    -Force `
    -ErrorAction SilentlyContinue




docker run --rm `
    -v "${CertVolume}:/certs:ro" `
    -v "${CaPath}:/output" `
    alpine:3.20 `
    sh -c "cp /certs/ca/ca.crt /output/ca.crt"

if ($LASTEXITCODE -ne 0) {
    throw "Failed to copy ca.crt"
}




docker run --rm `
    -v "${CertVolume}:/certs:ro" `
    -v "${ClientPath}:/output" `
    alpine:3.20 `
    sh -c "cp /certs/client/client.crt /output/client.crt && cp /certs/client/client.key /output/client.key"

if ($LASTEXITCODE -ne 0) {
    throw "Failed to copy client.crt/client.key"
}




Write-Host ""
Write-Host "Certificates updated:" -ForegroundColor Green
Write-Host ""
Write-Host "  $CaPath\ca.crt"
Write-Host "  $ClientPath\client.crt"
Write-Host "  $ClientPath\client.key"
Write-Host ""




Write-Host "Connecting to Redis..." -ForegroundColor Cyan
Write-Host ""

docker exec `
    -e "REDISCLI_AUTH=$RedisPassword" `
    -it redis `
    redis-cli `
    --tls `
    --cacert /certs/ca/ca.crt `
    --cert /certs/client/client.crt `
    --key /certs/client/client.key `
    --user $RedisUser