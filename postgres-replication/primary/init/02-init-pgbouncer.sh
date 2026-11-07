#!/bin/bash
set -e

echo "=== Creating PgBouncer user ==="

psql \
    -v ON_ERROR_STOP=1 \
    --username "$POSTGRES_USER" \
    --dbname "$POSTGRES_DB" \
    -v pgbouncer_user="$PGBOUNCER_USER" \
    -v pgbouncer_password="$PGBOUNCER_PASSWORD" \
    -v owner_user="$POSTGRES_USER" \
    <<'EOSQL'

SET password_encryption = 'scram-sha-256';

SELECT format(
    'CREATE ROLE %I WITH LOGIN PASSWORD %L',
    :'pgbouncer_user',
    :'pgbouncer_password'
)
WHERE NOT EXISTS (
    SELECT 1 FROM pg_roles WHERE rolname = :'pgbouncer_user'
)\gexec

ALTER ROLE :"pgbouncer_user" WITH LOGIN PASSWORD :'pgbouncer_password';

SELECT format(
    'GRANT CONNECT ON DATABASE %I TO %I',
    current_database(),
    :'pgbouncer_user'
)\gexec

GRANT USAGE ON SCHEMA public TO :"pgbouncer_user";

GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO :"pgbouncer_user";
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO :"pgbouncer_user";

ALTER DEFAULT PRIVILEGES FOR ROLE :"owner_user" IN SCHEMA public
    GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO :"pgbouncer_user";
ALTER DEFAULT PRIVILEGES FOR ROLE :"owner_user" IN SCHEMA public
    GRANT USAGE, SELECT ON SEQUENCES TO :"pgbouncer_user";

EOSQL

echo "=== PgBouncer user created successfully ==="