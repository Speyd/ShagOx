#!/bin/sh

set -eu

CA_DIR="/certs/ca"
SERVER_DIR="/certs/server"
CLIENT_DIR="/certs/client"

REDIS_USER="${REDIS_USER:-shagox}"

echo "========================================"
echo " Redis TLS initialization"
echo "========================================"

if [ -z "${REDIS_PASSWORD:-}" ]; then
    echo "ERROR: REDIS_PASSWORD is not set"
    exit 1
fi

mkdir -p \
    "$CA_DIR" \
    "$SERVER_DIR" \
    "$CLIENT_DIR"





echo ""
echo "[1/3] Checking Certificate Authority..."

if [ ! -f "$CA_DIR/ca.key" ] || [ ! -f "$CA_DIR/ca.crt" ]; then

    echo "Creating new Certificate Authority..."

    openssl genrsa \
        -out "$CA_DIR/ca.key" \
        4096

    openssl req \
        -x509 \
        -new \
        -sha256 \
        -days 3650 \
        -key "$CA_DIR/ca.key" \
        -out "$CA_DIR/ca.crt" \
        -subj "/C=DE/O=ShagOx/OU=Infrastructure/CN=ShagOx Redis CA" \
        -addext "basicConstraints=critical,CA:TRUE" \
        -addext "keyUsage=critical,keyCertSign,cRLSign" \
        -addext "subjectKeyIdentifier=hash"

    echo "CA created."

else

    echo "CA already exists."

fi





echo ""
echo "[2/3] Checking Redis server certificate..."

if [ ! -f "$SERVER_DIR/server.key" ] || [ ! -f "$SERVER_DIR/server.crt" ]; then

    echo "Creating Redis server certificate..."

    openssl genrsa \
        -out "$SERVER_DIR/server.key" \
        2048

    openssl req \
        -new \
        -sha256 \
        -key "$SERVER_DIR/server.key" \
        -out "$SERVER_DIR/server.csr" \
        -subj "/C=DE/O=ShagOx/OU=Redis/CN=redis"

    cat > /tmp/server.ext <<EOF
basicConstraints=critical,CA:FALSE
keyUsage=critical,digitalSignature,keyEncipherment
extendedKeyUsage=serverAuth
subjectAltName=DNS:redis,DNS:localhost,IP:127.0.0.1
subjectKeyIdentifier=hash
authorityKeyIdentifier=keyid,issuer
EOF

    openssl x509 \
        -req \
        -sha256 \
        -days 825 \
        -in "$SERVER_DIR/server.csr" \
        -CA "$CA_DIR/ca.crt" \
        -CAkey "$CA_DIR/ca.key" \
        -CAcreateserial \
        -out "$SERVER_DIR/server.crt" \
        -extfile /tmp/server.ext

    rm -f "$SERVER_DIR/server.csr"
    rm -f "$CA_DIR/ca.srl"

    echo "Redis server certificate created."

else

    echo "Redis server certificate already exists."

fi





echo ""
echo "[3/3] Checking Redis client certificate..."

if [ ! -f "$CLIENT_DIR/client.key" ] || [ ! -f "$CLIENT_DIR/client.crt" ]; then

    echo "Creating Redis client certificate..."

    openssl genrsa \
        -out "$CLIENT_DIR/client.key" \
        2048

    openssl req \
        -new \
        -sha256 \
        -key "$CLIENT_DIR/client.key" \
        -out "$CLIENT_DIR/client.csr" \
        -subj "/C=DE/O=ShagOx/OU=Redis Client/CN=redis-client"

    cat > /tmp/client.ext <<EOF
basicConstraints=critical,CA:FALSE
keyUsage=critical,digitalSignature,keyEncipherment
extendedKeyUsage=clientAuth
subjectKeyIdentifier=hash
authorityKeyIdentifier=keyid,issuer
EOF

    openssl x509 \
        -req \
        -sha256 \
        -days 825 \
        -in "$CLIENT_DIR/client.csr" \
        -CA "$CA_DIR/ca.crt" \
        -CAkey "$CA_DIR/ca.key" \
        -CAcreateserial \
        -out "$CLIENT_DIR/client.crt" \
        -extfile /tmp/client.ext

    rm -f "$CLIENT_DIR/client.csr"
    rm -f "$CA_DIR/ca.srl"

    echo "Redis client certificate created."

else

    echo "Redis client certificate already exists."

fi






echo ""
echo "Generating Redis ACL..."

PASSWORD_HASH=$(
    printf '%s' "$REDIS_PASSWORD" |
    sha256sum |
    awk '{print $1}'
)

cat > /redis/users.acl <<EOF
user default off
user ${REDIS_USER} on #${PASSWORD_HASH} ~* +@all
EOF

chmod 644 /redis/users.acl

echo "ACL generated for user: ${REDIS_USER}"






echo ""
echo "Setting permissions..."

chmod 644 "$CA_DIR/ca.crt"
chmod 644 "$SERVER_DIR/server.crt"
chmod 644 "$SERVER_DIR/server.key"
chmod 644 "$CLIENT_DIR/client.crt"
chmod 644 "$CLIENT_DIR/client.key"

chmod 600 "$CA_DIR/ca.key"






echo ""
echo "Verifying CA..."

openssl x509 \
    -in "$CA_DIR/ca.crt" \
    -noout \
    -subject \
    -issuer \
    -ext basicConstraints


echo ""
echo "Verifying Redis server certificate..."

openssl x509 \
    -in "$SERVER_DIR/server.crt" \
    -noout \
    -subject \
    -issuer \
    -ext subjectAltName \
    -ext extendedKeyUsage


echo ""
echo "Verifying server certificate chain..."

openssl verify \
    -CAfile "$CA_DIR/ca.crt" \
    "$SERVER_DIR/server.crt"


echo ""
echo "Verifying client certificate chain..."

openssl verify \
    -CAfile "$CA_DIR/ca.crt" \
    "$CLIENT_DIR/client.crt"





echo ""
echo "Checking generated files..."

test -f "$CA_DIR/ca.crt"
test -f "$CA_DIR/ca.key"

test -f "$SERVER_DIR/server.crt"
test -f "$SERVER_DIR/server.key"

test -f "$CLIENT_DIR/client.crt"
test -f "$CLIENT_DIR/client.key"

test -f /redis/users.acl

echo "All required files exist."

echo ""
echo "========================================"
echo " Redis initialization completed"
echo "========================================"