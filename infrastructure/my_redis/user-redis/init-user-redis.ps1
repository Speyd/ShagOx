$ErrorActionPreference = "Stop"

$SCRIPT_DIR = $PSScriptRoot

$ACL_DIR = [System.IO.Path]::GetFullPath(
    (Join-Path $SCRIPT_DIR "../acl/user-redis")
)

$CERT_DIR = [System.IO.Path]::GetFullPath(
    (Join-Path $SCRIPT_DIR "../certs/user-redis")
)

$REDIS_USER = if ($env:REDIS_USER) {
    $env:REDIS_USER
} else {
    "shagox_user"
}

$REDIS_PASSWORD = $env:REDIS_USER_PASSWORD

if ([string]::IsNullOrEmpty($REDIS_PASSWORD)) {
    Write-Error "ERROR: REDIS_USER_PASSWORD is not set"
    exit 1
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

if (-not (Test-Path (Join-Path $CERT_DIR "server.crt") -PathType Leaf)) {
    throw "Certificate not found: $(Join-Path $CERT_DIR 'server.crt')"
}

if (-not (Test-Path (Join-Path $CERT_DIR "server.key") -PathType Leaf)) {
    throw "Private key not found: $(Join-Path $CERT_DIR 'server.key')"
}

Write-Host "User Redis initialized."