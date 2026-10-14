$ErrorActionPreference = "Stop"

$SCRIPT_DIR = $PSScriptRoot

$CERTS_DIR = [System.IO.Path]::GetFullPath(
    (Join-Path $SCRIPT_DIR "../certs")
)

# Remove all existing certificates so everything is regenerated
Write-Host "Removing existing certificates..."

if (Test-Path $CERTS_DIR) {
    Remove-Item $CERTS_DIR -Recurse -Force
}

$CA_DIR = Join-Path $CERTS_DIR "ca"
$REDIS_SERVER_DIR = Join-Path $CERTS_DIR "redis"
$USER_REDIS_SERVER_DIR = Join-Path $CERTS_DIR "user-redis"
$CLIENT_DIR = Join-Path $CERTS_DIR "client"

# Execute OpenSSL and check its exit code
function Invoke-OpenSSL {
    param(
        [Parameter(Mandatory)]
        [string[]]$Arguments
    )

    & openssl @Arguments

    if ($LASTEXITCODE -ne 0) {
        throw "OpenSSL failed with exit code $LASTEXITCODE"
    }
}

# Create directories
@(
    $CA_DIR
    $REDIS_SERVER_DIR
    $USER_REDIS_SERVER_DIR
    $CLIENT_DIR
) | ForEach-Object {
    New-Item -ItemType Directory -Path $_ -Force | Out-Null
}

# --------------------------------------------------
# 1. Create Certificate Authority (CA)
# --------------------------------------------------

Write-Host "[1/3] Creating CA..."

$CA_KEY = Join-Path $CA_DIR "ca.key"
$CA_CERT = Join-Path $CA_DIR "ca.crt"

Invoke-OpenSSL -Arguments @(
    "genrsa",
    "-out", $CA_KEY,
    "4096"
)

Invoke-OpenSSL -Arguments @(
    "req",
    "-x509",
    "-new",
    "-sha256",
    "-days", "3650",
    "-key", $CA_KEY,
    "-out", $CA_CERT,
    "-subj", "/C=DE/O=ShagOx/OU=Infrastructure/CN=ShagOx Redis CA",
    "-addext", "basicConstraints=critical,CA:TRUE",
    "-addext", "keyUsage=critical,keyCertSign,cRLSign",
    "-addext", "subjectKeyIdentifier=hash"
)

Write-Host "CA created."

# --------------------------------------------------
# 2. Create Redis server certificates
# --------------------------------------------------

function New-RedisServerCertificate {
    param(
        [Parameter(Mandatory)]
        [string]$ServerName,

        [Parameter(Mandatory)]
        [string]$ServerDir
    )

    $SERVER_CERT = Join-Path $ServerDir "server.crt"
    $SERVER_KEY = Join-Path $ServerDir "server.key"

    Write-Host "Creating certificate for $ServerName..."

    $SERVER_CSR = Join-Path $ServerDir "server.csr"
    $SERVER_EXT = Join-Path $ServerDir "server.ext"
    $CA_SERIAL = Join-Path $CA_DIR "ca.srl"

    Invoke-OpenSSL -Arguments @(
        "genrsa",
        "-out", $SERVER_KEY,
        "2048"
    )

    Invoke-OpenSSL -Arguments @(
        "req",
        "-new",
        "-sha256",
        "-key", $SERVER_KEY,
        "-out", $SERVER_CSR,
        "-subj", "/C=DE/O=ShagOx/OU=Redis/CN=$ServerName"
    )

    $EXT_CONTENT = @"
basicConstraints=critical,CA:FALSE
keyUsage=critical,digitalSignature,keyEncipherment
extendedKeyUsage=serverAuth
subjectAltName=DNS:$ServerName,DNS:localhost,IP:127.0.0.1
subjectKeyIdentifier=hash
authorityKeyIdentifier=keyid,issuer
"@

    [System.IO.File]::WriteAllText(
        $SERVER_EXT,
        $EXT_CONTENT + "`n",
        [System.Text.Encoding]::ASCII
    )

    Invoke-OpenSSL -Arguments @(
        "x509",
        "-req",
        "-sha256",
        "-days", "825",
        "-in", $SERVER_CSR,
        "-CA", $CA_CERT,
        "-CAkey", $CA_KEY,
        "-CAcreateserial",
        "-out", $SERVER_CERT,
        "-extfile", $SERVER_EXT
    )

    Remove-Item $SERVER_CSR, $SERVER_EXT -Force
    Remove-Item $CA_SERIAL -Force -ErrorAction SilentlyContinue

    Invoke-OpenSSL -Arguments @(
        "verify",
        "-CAfile", $CA_CERT,
        $SERVER_CERT
    )

    Write-Host "Certificate for $ServerName created and verified."
}

Write-Host "[2/3] Creating Redis server certificates..."

New-RedisServerCertificate `
    -ServerName "redis" `
    -ServerDir $REDIS_SERVER_DIR

New-RedisServerCertificate `
    -ServerName "user-redis" `
    -ServerDir $USER_REDIS_SERVER_DIR

# --------------------------------------------------
# 3. Create client certificate
# --------------------------------------------------

Write-Host "[3/3] Creating client certificate..."

$CLIENT_KEY = Join-Path $CLIENT_DIR "client.key"
$CLIENT_CERT = Join-Path $CLIENT_DIR "client.crt"

$CLIENT_CSR = Join-Path $CLIENT_DIR "client.csr"
$CLIENT_EXT = Join-Path $CLIENT_DIR "client.ext"
$CA_SERIAL = Join-Path $CA_DIR "ca.srl"

Invoke-OpenSSL -Arguments @(
    "genrsa",
    "-out", $CLIENT_KEY,
    "2048"
)

Invoke-OpenSSL -Arguments @(
    "req",
    "-new",
    "-sha256",
    "-key", $CLIENT_KEY,
    "-out", $CLIENT_CSR,
    "-subj", "/C=DE/O=ShagOx/OU=Redis Client/CN=redis-client"
)

$CLIENT_EXT_CONTENT = @"
basicConstraints=critical,CA:FALSE
keyUsage=critical,digitalSignature
extendedKeyUsage=clientAuth
subjectKeyIdentifier=hash
authorityKeyIdentifier=keyid,issuer
"@

[System.IO.File]::WriteAllText(
    $CLIENT_EXT,
    $CLIENT_EXT_CONTENT + "`n",
    [System.Text.Encoding]::ASCII
)

Invoke-OpenSSL -Arguments @(
    "x509",
    "-req",
    "-sha256",
    "-days", "825",
    "-in", $CLIENT_CSR,
    "-CA", $CA_CERT,
    "-CAkey", $CA_KEY,
    "-CAcreateserial",
    "-out", $CLIENT_CERT,
    "-extfile", $CLIENT_EXT
)

Remove-Item $CLIENT_CSR, $CLIENT_EXT -Force
Remove-Item $CA_SERIAL -Force -ErrorAction SilentlyContinue

# Verify client certificate
Invoke-OpenSSL -Arguments @(
    "verify",
    "-CAfile", $CA_CERT,
    $CLIENT_CERT
)

Write-Host "Client certificate created and verified."

# --------------------------------------------------
# Finished
# --------------------------------------------------

Write-Host ""
Write-Host "Certificate initialization completed successfully."
Write-Host "All certificates and keys have been regenerated."