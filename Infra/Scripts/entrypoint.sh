#!/bin/bash
/opt/mssql/bin/sqlservr &

sleep 30s

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$MSSQL_SA_PASSWORD" -d master -i /init.sql


wait