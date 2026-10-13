$ErrorActionPreference = "Stop"

$SCRIPT_DIR = $PSScriptRoot

$ACL_DIR = [System.IO.Path]::GetFullPath(
    (Join-Path $SCRIPT_DIR "../acl/redis")
)

$CERT_DIR = [System.IO.Path]::GetFullPath(
    (Join-Path $SCRIPT_DIR "../certs/redis")
)

$REDIS_USER = if ($env:REDIS_DATA_USER) {
    $env:REDIS_DATA_USER
} else {
    "shagox"
}

$REDIS_PASSWORD = $env:REDIS_DATA_PASSWORD

if ([string]::IsNullOrEmpty($REDIS_PASSWORD)) {
    Write-Error "ERROR: REDIS_DATA_PASSWORD is not set"
    exit 1
}

$CERT_FILE = Join-Path $CERT_DIR "server.crt"
$KEY_FILE = Join-Path $CERT_DIR "server.key"

if (-not (Test-Path $CERT_FILE -PathType Leaf)) {
    throw "Certificate not found: $CERT_FILE"
}

if (-not (Test-Path $KEY_FILE -PathType Leaf)) {
    throw "Private key not found: $KEY_FILE"
}

New-Item -ItemType Directory -Path $ACL_DIR -Force | Out-Null

$sha256 = [System.Security.Cryptography.SHA256]::Create()

try {
    $passwordBytes = [System.Text.Encoding]::UTF8.GetBytes($REDIS_PASSWORD)
    $hashBytes = $sha256.ComputeHash($passwordBytes)
    $PASSWORD_HASH = [System.BitConverter]::ToString($hashBytes).Replace("-", "").ToLowerInvariant()
}
finally {
    $sha256.Dispose()
}

$ACL_CONTENT = @"
user default off
user ${REDIS_USER} on #${PASSWORD_HASH} ~* +@all
"@

$ACL_FILE = Join-Path $ACL_DIR "users.acl"

[System.IO.File]::WriteAllText(
    $ACL_FILE,
    $ACL_CONTENT + "`n",
    [System.Text.UTF8Encoding]::new($false)
)

Write-Host "Main Redis initialized."