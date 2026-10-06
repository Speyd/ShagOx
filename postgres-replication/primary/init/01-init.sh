#!/bin/bash

set -e

psql -v ON_ERROR_STOP=1 \
    --username "$POSTGRES_USER" \
    --dbname "$POSTGRES_DB" <<-EOSQL

    CREATE ROLE ${REPLICATOR_USER}
        WITH REPLICATION
        LOGIN
        PASSWORD '${REPLICATOR_PASSWORD}';

EOSQL