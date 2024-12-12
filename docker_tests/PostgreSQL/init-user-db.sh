#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE USER docker WITH SUPERUSER PASSWORD 'jw8s0F4';
	CREATE DATABASE docker;
	GRANT ALL PRIVILEGES ON DATABASE docker TO docker;

  ALTER USER postgres WITH PASSWORD 'admin2';
EOSQL

sed -i 's/host all all all scram-sha-256/host all all all trust/' /var/lib/postgresql/data/pg_hba.conf

psql -U postgres -c "SELECT pg_reload_conf();"