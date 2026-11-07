$ErrorActionPreference = "Stop"

$scriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path
$postgresRoot = Resolve-Path (Join-Path $scriptDirectory "..")
$projectRoot = Resolve-Path (Join-Path $postgresRoot "../..")

$envFile = Join-Path $projectRoot ".env"
$outputFile = Join-Path $postgresRoot "userlist.txt"

if (-not (Test-Path $envFile)) {
    throw ".env not found: $envFile"
}

function Get-EnvValue {
    param([string]$Name)

    $line = Get-Content $envFile |
        Where-Object { $_ -match "^$Name=" } |
        Select-Object -First 1

    if (-not $line) {
        throw "$Name not found in .env"
    }

    return ($line -replace "^$Name=", "").Trim()
}

$postgresUser      = Get-EnvValue "POSTGRES_USER"
$postgresPassword  = Get-EnvValue "POSTGRES_PASSWORD"
$pgbouncerUser     = Get-EnvValue "PGBOUNCER_USER"
$pgbouncerPassword = Get-EnvValue "PGBOUNCER_PASSWORD"

Write-Host "=== Getting PostgreSQL SCRAM verifier ==="

$query = @"
SELECT rolpassword
FROM pg_authid
WHERE rolname = '$($pgbouncerUser.Replace("'", "''"))';
"@

$verifier = docker exec `
    -e "PGPASSWORD=$postgresPassword" `
    postgres-primary `
    psql `
    -U $postgresUser `
    -d postgres `
    -tAc $query

if ($LASTEXITCODE -ne 0) {
    throw "Failed to retrieve SCRAM verifier from PostgreSQL."
}

$verifier = $verifier.Trim()

if ([string]::IsNullOrWhiteSpace($verifier)) {
    throw "PostgreSQL user '$pgbouncerUser' does not exist or has no password."
}

if (-not $verifier.StartsWith("SCRAM-SHA-256$")) {
    throw "User '$pgbouncerUser' does not have a SCRAM-SHA-256 verifier."
}

if (-not (Test-Path $postgresRoot)) {
    New-Item -ItemType Directory -Path $postgresRoot | Out-Null
}

$escapedUser     = $pgbouncerUser.Replace('\', '\\').Replace('"', '\"')
$escapedVerifier = $verifier.Replace('\', '\\').Replace('"', '\"')

$content = '"{0}" "{1}"' -f $escapedUser, $escapedVerifier

[System.IO.File]::WriteAllText(
    $outputFile,
    $content + "`n",
    [System.Text.UTF8Encoding]::new($false)
)

Write-Host "PgBouncer SCRAM userlist generated successfully."
Write-Host "Output: $outputFile"