#!/bin/bash

set -e

echo "=== Creating replication user ==="

psql \
    -v ON_ERROR_STOP=1 \
    --username "$POSTGRES_USER" \
    --dbname "$POSTGRES_DB" \
    -v replicator_user="$REPLICATOR_USER" \
    -v replicator_password="$REPLICATOR_PASSWORD" \
    <<'EOSQL'

SELECT format(
    'CREATE ROLE %I WITH REPLICATION LOGIN PASSWORD %L',
    :'replicator_user',
    :'replicator_password'
)
WHERE NOT EXISTS (
    SELECT 1
    FROM pg_roles
    WHERE rolname = :'replicator_user'
)\gexec

EOSQL

echo "=== Replication user created ==="